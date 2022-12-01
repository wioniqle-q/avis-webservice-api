
using Avis.Services.OrganizationModel;

namespace Avis.Features.Features.Queries.MapQueryOrganization;

public record QueryOrganizationCommandResponse 
{
    public List<OrganizationUser> organizationUsers { get; set; }
}
