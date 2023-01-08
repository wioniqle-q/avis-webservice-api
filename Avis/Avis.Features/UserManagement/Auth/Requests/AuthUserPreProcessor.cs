using MediatR.Pipeline;

namespace Avis.Features.UserManagement.Auth.Requests;

public class AuthUserPreProcessor : IRequestPreProcessor<AuthUserRequest>
{
    public Task Process(AuthUserRequest request, CancellationToken cancellationToken)
    {
        // TODO: Perform any pre-processing tasks here, such as logging or data preparation
        return Task.CompletedTask;
    }
}