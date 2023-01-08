using Avis.Features.UserManagement.Auth.Requests;
using MediatR.Pipeline;

namespace Avis.Features.UserManagement.Auth.Handlers;

public class AuthUserPostProcessor : IRequestPostProcessor<AuthUserRequest, AuthUserResponse>
{
    public Task Process(AuthUserRequest request, AuthUserResponse response, CancellationToken cancellationToken)
    {
        // Perform any post-processing tasks here, such as logging or sending notifications
        return Task.CompletedTask;
    }
}