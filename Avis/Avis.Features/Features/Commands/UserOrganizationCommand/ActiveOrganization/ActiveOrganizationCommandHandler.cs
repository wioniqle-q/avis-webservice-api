
using Avis.Services.DataConsultion.Consultion;
using Avis.Services.OrganizationModel;
using MediatR;

namespace Avis.Features.Features.Commands.UserOrganizationCommand.ActiveOrganization;

public class ActiveOrganizationCommandHandler : IRequestHandler<ActiveOrganizationCommandRequest, ActiveOrganizationCommandResponse>
{
    private protected AccountConsultion accountConsultion { get; }

    public ActiveOrganizationCommandHandler(AccountConsultion accountConsultion)
    {
        this.accountConsultion = accountConsultion;
    }

    public virtual async Task<ActiveOrganizationCommandResponse> Handle(ActiveOrganizationCommandRequest request, CancellationToken cancellationToken)
    {
        var organization = new OrganizationUser(request.Name, string.Empty);

        var organizationUser = await this.accountConsultion.OrganizationUserActivateAsync(organization);

        return new ActiveOrganizationCommandResponse() { Result = await organizationUser };
    }
}