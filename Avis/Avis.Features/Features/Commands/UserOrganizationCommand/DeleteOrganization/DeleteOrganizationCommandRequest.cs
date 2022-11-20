using MediatR;

namespace Avis.Features.Features.Commands.UserOrganizationCommand.DeleteOrganization;

public struct DeleteOrganizationCommandRequest : IRequest<DeleteOrganizationCommandResponse>
{
    public DeleteOrganizationCommandRequest() { }
    
    public string Name { get; set; } = string.Empty;
}

