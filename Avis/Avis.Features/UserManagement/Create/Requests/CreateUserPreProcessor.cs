using MediatR.Pipeline;

namespace Avis.Features.UserManagement.Create.Requests;
public class CreateUserPreProcessor : IRequestPreProcessor<CreateUserRequest>
{
    public Task Process(CreateUserRequest request, CancellationToken cancellationToken)
    {
        // TODO: Perform any pre-processing tasks here, such as logging or data preparation
        return Task.CompletedTask;
    }
}
