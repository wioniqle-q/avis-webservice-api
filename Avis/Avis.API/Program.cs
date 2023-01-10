using Avis.DB.Configurations;
using Avis.DB.Interfaces;
using Avis.DB.Modules;
using Avis.DB.Options;
using Avis.Features.UserManagement.Auth.Handlers;
using Avis.Features.UserManagement.Auth.Requests;
using Avis.Features.UserManagement.Create.Requests;
using Avis.Features.UserManagement.Handlers;
using Avis.Services.DataConsultion.Consultion;
using MediatR.Extensions.FluentValidation.AspNetCore;
using MediatR.Pipeline;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.AddServerHeader = false;
});

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
    .AllowAnyMethod().AllowAnyHeader().AllowCredentials());
});

ThreadPool.SetMaxThreads(Environment.ProcessorCount, Environment.ProcessorCount);

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = 429;
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
    RateLimitPartition.GetFixedWindowLimiter(
        partitionKey: httpContext.Request.Headers["CF-Connecting-IP"].ToString(),
        factory: partition => new FixedWindowRateLimiterOptions
        {
            AutoReplenishment = true,
            PermitLimit = 15,
            Window = TimeSpan.FromSeconds(30)
        })
    );
});

var config = new MapperConfiguration(conf =>
{
    conf.AddProfile<ObjectClassProfile>();
});

var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.GetTypes().Any(t => t.GetInterfaces().Any(i => i == typeof(IMediator))));

foreach (var assembly in assemblies)
{
    Console.WriteLine(assembly);
    builder.Services.AddMediatR(assembly);
}
builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddMediatR(typeof(CreateUserByIdValidator).Assembly);
builder.Services.AddMediatR(typeof(AuthUserByIdValidator).Assembly);

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestExceptionProcessorBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CreateUserPipelineBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthUserPipelineBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddFluentValidation(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped(s => config.CreateMapper());
builder.Services.AddSingleton<IMongoDbModule, MongoDbModule>(conf => {
    var mongoDbModule = new MongoDbModule();
    mongoDbModule.ConfigureServices(builder.Services);
    return mongoDbModule;
});
builder.Services.Configure<MongoDbOptions>(options => {
    options.ConnectionString = builder.Configuration.GetSection("MongoDbOptions:ConnectionString").Value;
    options.DatabaseName = builder.Configuration.GetSection("MongoDbOptions:DatabaseName").Value;
    options.UserModelDatabaseName = builder.Configuration.GetSection("MongoDbOptions:UserModelDatabaseName").Value;
});
builder.Services.AddSingleton<MongoDbConfigurations>();
builder.Services.AddSingleton<AuthService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddMemoryCache();
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlerActions>();
app.UseMiddleware<BodyHandlerActions>();
app.UseSecurityHeaders(SecurityPolicy.HeaderPolicyCollection());
app.UseHsts();
app.UseRateLimiter();
app.MapControllers();

await app.RunAsync();
