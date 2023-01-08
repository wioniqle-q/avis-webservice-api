using Avis.Features.UserManagement.Auth.Requests;
using Avis.Services.DataConsultion.Consultion;
using Avis.Services.Models;
using MediatR;

namespace Avis.Features.UserManagement.Auth.Handlers;
public class AuthUserHandler : IRequestHandler<AuthUserRequest, AuthUserResponse>
{
    private readonly AuthService _authService;
    
    public AuthUserHandler(AuthService authService)
    {
        _authService = authService;
    }

    public async Task<AuthUserResponse> Handle(AuthUserRequest request, CancellationToken cancellationToken)
    {
        var serviceResult = await this._authService.AuthenticateUserAsync(
            new DerivedUserVirtualClass() { UserName = request.UserName, Password = request.Password },
            cancellationToken
        );

        return await Task.FromResult(new AuthUserResponse() { Response = string.Concat(serviceResult.Response) });
    }
}