using Avis.Features.UserManagement.Auth.Requests;
using MediatR.Pipeline;

namespace Avis.Features.UserManagement.Auth.Handlers;
public class AuthUserExceptionHandler : IRequestExceptionHandler<AuthUserRequest, AuthUserResponse>
{
    public Task Handle(AuthUserRequest request, Exception exception, RequestExceptionHandlerState<AuthUserResponse> state, CancellationToken cancellationToken)
    {
        // Handle any exceptions thrown during the handling of the request
        return Task.CompletedTask;
    }
}
