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
        var organization = new OrganizationUser
        {
            Name = request.Name,
            Password = request.Password
        };

        var organizationUser = await accountConsultion.OrganizationUserFindAsync(organization);
        if (organizationUser is not null)
        {
            return new CreateOrganizationCommandResponse
            {
                Result = "Organization already exists"
            };
        }

        await accountConsultion.OrganizationUserCreateAsync(organization);

        return await Task.FromResult(new CreateOrganizationCommandResponse
        {
            Result = "Organization created successfully"
        });
    }
}