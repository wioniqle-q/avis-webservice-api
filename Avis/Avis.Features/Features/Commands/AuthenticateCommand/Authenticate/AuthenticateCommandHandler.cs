using Avis.Services.DataConsultion.Consultion;
using Avis.Services.OrganizationModel;
using MediatR;

namespace Avis.Features.Features.Commands.AuthenticateCommand.Authenticate;

public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommandRequest, AuthenticateCommandResponse>
{
    protected private readonly AuthenticateConsultion authenticateConsultion;

    public AuthenticateCommandHandler(AuthenticateConsultion authenticateConsultion)
    {
        this.authenticateConsultion = authenticateConsultion;
    }

    public virtual async Task<AuthenticateCommandResponse> Handle(AuthenticateCommandRequest request, CancellationToken cancellationToken)
    {
        var userProperty = new OrganizationUser(request.Name, request.Password);

        var user = await authenticateConsultion.AuthenticateAsync(userProperty, cancellationToken);

        return new AuthenticateCommandResponse { Result = user };
    }
}