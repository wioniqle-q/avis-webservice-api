using AutoMapper;
using Avis.Services.DataConsultion.Consultion;
using Avis.Services.OrganizationModel;
using MediatR;

namespace Avis.Features.Features.Queries.MapQueryOrganization;

public class QueryOrganizationCommandHandler : IRequestHandler<QueryOrganizationCommandRequest, QueryOrganizationCommandResponse>
{
    private readonly AccountConsultion _accountConsultion;
    private readonly IMapper _mapper;

    public QueryOrganizationCommandHandler(AccountConsultion accountConsultion, IMapper mapper)
    {
        _accountConsultion = accountConsultion;
        _mapper = mapper;
    }

    public async Task<QueryOrganizationCommandResponse> Handle(QueryOrganizationCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _accountConsultion.OrganizationUserMapAsync(cancellationToken);

        var response = _mapper.Map<List<OrganizationUser>>(result);
        response.Select(x => new
        {
            x.OrganizationId,
            x.Name,
            x.IsActive,
            x.IsDeleted,
            x.CreatedAt
        }).Aggregate((a, b) => new
        {
            a.OrganizationId,
            a.Name,
            a.IsActive,
            a.IsDeleted,
            a.CreatedAt
        });

        return new QueryOrganizationCommandResponse
        {
            organizationUsers = response
        };
    }
}


