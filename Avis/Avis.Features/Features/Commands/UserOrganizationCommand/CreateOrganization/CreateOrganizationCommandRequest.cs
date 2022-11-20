using MediatR;

namespace Avis.Features.Features.Commands.UserOrganizationCommand.CreateOrganization;

public struct CreateOrganizationCommandRequest : IRequest<CreateOrganizationCommandResponse>
{
    public CreateOrganizationCommandRequest() { }
    
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
