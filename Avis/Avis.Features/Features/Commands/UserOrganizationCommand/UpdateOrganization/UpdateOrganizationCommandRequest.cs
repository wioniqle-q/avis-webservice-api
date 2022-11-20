
using MediatR;

namespace Avis.Features.Features.Commands.UserOrganizationCommand.UpdateOrganization;

public struct UpdateOrganizationCommandRequest : IRequest<UpdateOrganizationCommandResponse>
{
    public string Name { get; set; }
    public string Password { get; set; }
}
