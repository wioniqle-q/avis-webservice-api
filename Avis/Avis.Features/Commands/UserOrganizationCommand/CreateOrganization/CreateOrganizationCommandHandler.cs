using Avis.Services.DataConsultion.Consultion;
using Avis.Services.OrganizationModel;
using MediatR;

namespace Avis.Features.Features.Commands.UserOrganizationCommand.CreateOrganization;

public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommandRequest, CreateOrganizationCommandResponse>
{
    private readonly AccountConsultion accountConsultion;

    public CreateOrganizationCommandHandler(AccountConsultion accountConsultion)
    {
        this.accountConsultion = accountConsultion;
    }

    public virtual async Task<CreateOrganizationCommandResponse> Handle(CreateOrganizationCommandRequest request, CancellationToken cancellationToken)
    {
        var organization = new OrganizationUser(request.Name, request.Password);

        var organizationUser = await accountConsultion.OrganizationUserCreateAsync(organization, cancellationToken);

        return await Task.FromResult(new CreateOrganizationCommandResponse
        {
            Result = await organizationUser
        });
    }
}