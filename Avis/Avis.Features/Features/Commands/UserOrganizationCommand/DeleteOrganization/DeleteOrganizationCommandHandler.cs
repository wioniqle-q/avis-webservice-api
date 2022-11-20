using Avis.Services.DataConsultion.Consultion;
using Avis.Services.OrganizationModel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Avis.Features.Features.Commands.UserOrganizationCommand.DeleteOrganization;

public class DeleteOrganizationCommandHandler : IRequestHandler<DeleteOrganizationCommandRequest, DeleteOrganizationCommandResponse>
{
    private readonly AccountConsultion _accountConsultion;
    public DeleteOrganizationCommandHandler(AccountConsultion consultion)
    {
        this._accountConsultion = consultion;
    }

    public async Task<DeleteOrganizationCommandResponse> Handle(DeleteOrganizationCommandRequest request, CancellationToken cancellationToken)
    {
        var organization = new OrganizationUser(request.Name, string.Empty);
        
        var deleteResult = await this._accountConsultion.OrganizationUserDeactivateAsync(organization, cancellationToken);

        return new DeleteOrganizationCommandResponse { Result = await deleteResult };
    }
}