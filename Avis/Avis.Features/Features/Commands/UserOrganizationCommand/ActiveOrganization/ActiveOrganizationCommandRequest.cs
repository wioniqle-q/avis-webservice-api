
using MediatR;

namespace Avis.Features.Features.Commands.UserOrganizationCommand.ActiveOrganization;

public struct ActiveOrganizationCommandRequest: IRequest<ActiveOrganizationCommandResponse>
{
    public string Name { get; set; }
}
