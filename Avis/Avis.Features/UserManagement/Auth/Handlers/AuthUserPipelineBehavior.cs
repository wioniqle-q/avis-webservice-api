using MediatR;

namespace Avis.Features.UserManagement.Auth.Handlers;

public class AuthUserPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<IRequest<TResponse>, TResponse>
{
    public async Task<TResponse> Handle(IRequest<TResponse> request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Perform any additional processing before the request is handled
        var response = await next();
        // Perform any additional processing after the request has been handled
        return response;
    }
}