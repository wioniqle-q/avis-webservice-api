using AutoMapper;
using Avis.Features.Features.Queries.MapQueryOrganization;
using Avis.Services.OrganizationModel;

namespace Avis.Services.Contract.Mapper;

public class ObjectClassProfile : Profile
{
    public ObjectClassProfile()
    {
        CreateMap<OrganizationUser, QueryOrganizationCommandResponse>();
    }
}
