using Avis.Features.UserManagement.Create.Requests;
using Avis.Services.DataConsultion.Consultion;
using Avis.Services.Models;
using MediatR;

namespace Avis.Features.UserManagement.Handlers;
public class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
{
    private readonly UserService _userService;

    public CreateUserHandler(UserService userService)
    {
        _userService = userService;
    }

    public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var serviceResult = await this._userService.InsertUserModelAsync(
            new DerivedUserVirtualClass() { UserName = request.UserName, Password = request.Password },
            cancellationToken
        );

        return await Task.FromResult(new CreateUserResponse() { Response = string.Concat(await serviceResult) });
    }
}
