using Avis.Features.UserManagement.Create.Requests;
using MediatR.Pipeline;

namespace Avis.Features.UserManagement.Handlers;
public class CreateUserExceptionHandler : IRequestExceptionHandler<CreateUserRequest, CreateUserResponse>
{
    public Task Handle(CreateUserRequest request, Exception exception, RequestExceptionHandlerState<CreateUserResponse> state, CancellationToken cancellationToken)
    {
        // Handle any exceptions thrown during the handling of the request
        return Task.CompletedTask;
    }
}
