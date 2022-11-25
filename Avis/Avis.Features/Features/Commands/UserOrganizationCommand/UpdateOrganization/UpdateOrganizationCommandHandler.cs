
using Avis.Services.DataConsultion.Consultion;
using Avis.Services.OrganizationModel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Avis.Features.Features.Commands.UserOrganizationCommand.UpdateOrganization;

public class UpdateOrganizationCommandHandler : IRequestHandler<UpdateOrganizationCommandRequest, UpdateOrganizationCommandResponse>
{
    private readonly AccountConsultion _accountConsultion;

    public UpdateOrganizationCommandHandler(AccountConsultion consultion)
    {
        this._accountConsultion = consultion;
    }

    public async Task<UpdateOrganizationCommandResponse> Handle(UpdateOrganizationCommandRequest request, CancellationToken cancellationToken)
    {
        var organization = new OrganizationUser(request.Name, request.Password);

        var updateResult = await this._accountConsultion.OrganizationUserUpdateAsync(organization, cancellationToken);

        return new UpdateOrganizationCommandResponse { Result = await updateResult };
    }
}