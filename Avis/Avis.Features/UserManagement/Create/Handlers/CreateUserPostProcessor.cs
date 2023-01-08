using Avis.Features.UserManagement.Create.Requests;
using MediatR.Pipeline;

namespace Avis.Features.UserManagement.Handlers;
public class CreateUserPostProcessor : IRequestPostProcessor<CreateUserRequest, CreateUserResponse>
{
    public Task Process(CreateUserRequest request, CreateUserResponse response, CancellationToken cancellationToken)
    {
        // Perform any post-processing tasks here, such as logging or sending notifications
        return Task.CompletedTask;
    }
}
