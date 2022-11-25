using Avis.Features.Features.Commands.AuthenticateCommand.Authenticate;
using Avis.Services.DT.DT.Algorithm;

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
            PermitLimit = 5,
            Window = TimeSpan.FromSeconds(10)
        })
    );
});

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<IAvisMongoDbContext, AvisMongoDbContext>();
builder.Services.AddSingleton<AccountConsultion>();
builder.Services.AddSingleton<AuthenticateConsultion>();
builder.Services.AddSingleton<AuthenticateCommandHandler>();
builder.Services.AddSingleton<CreateOrganizationCommandHandler>();
builder.Services.AddSingleton<ProtectDT>();
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
