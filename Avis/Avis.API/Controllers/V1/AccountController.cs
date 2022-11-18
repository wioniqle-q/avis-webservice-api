using Avis.Features.Features.Commands.UserOrganizationCommand.CreateOrganization;
using Avis.Services.DataConsultion.Consultion;
using Avis.Services.OrganizationModel;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace Avis.API.Controllers.V1;

[Area("account")]
[Route("api/v{version:apiVersion}/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private protected AccountConsultion accountConsultion { get; }
    private protected IMediator mediator { get; }

    public AccountController(AccountConsultion accountConsultion, IMediator mediator)
    {
        this.accountConsultion = accountConsultion;
        this.mediator = mediator;
    }

    [HttpPost]
    [Route("create-organization")]
    public virtual async Task<IActionResult> CreateOrganization([FromBody] [NotNull]OrganizationUser organization)
    {
        var result = await this.mediator.Send(new CreateOrganizationCommandRequest
        {
            Name = organization.Name,
            Password = organization.Password
        });

        return Ok(result);
    }
}