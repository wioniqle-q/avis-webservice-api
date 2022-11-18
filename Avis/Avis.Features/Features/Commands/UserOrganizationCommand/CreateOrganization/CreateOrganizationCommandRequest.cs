using MediatR;
using MongoDB.Bson;

namespace Avis.Features.Features.Commands.UserOrganizationCommand.CreateOrganization;

public struct CreateOrganizationCommandRequest : IRequest<CreateOrganizationCommandResponse>
{
    public CreateOrganizationCommandRequest()
    {
        
    }

    public ObjectId OrganizationId { get; set; } = ObjectId.GenerateNewId();

    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string? CreatedAt { get; set; } = DateTime.Now.ToString();
    public string? HardwareId { get; set; } = null;
    public string? Role { get; } = "OrganizationUser";

    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;
}
