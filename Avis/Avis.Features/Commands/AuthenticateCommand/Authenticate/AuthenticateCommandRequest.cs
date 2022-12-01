using MediatR;

namespace Avis.Features.Features.Commands.AuthenticateCommand.Authenticate;

public struct AuthenticateCommandRequest : IRequest<AuthenticateCommandResponse>
{
    public string Name { get; set; }
    public string Password { get; set; }
}
