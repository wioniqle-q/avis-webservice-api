using Avis.Features.Features.Commands.UserOrganizationCommand.ActiveOrganization;
using Avis.Features.Features.Commands.UserOrganizationCommand.DeleteOrganization;
using Avis.Features.Features.Commands.UserOrganizationCommand.UpdateOrganization;
using Avis.Features.Features.Queries.MapQueryOrganization;

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
    public virtual async Task<IActionResult> CreateOrganization([FromBody] OrganizationUser organization)
    {
        var mediator = await this.mediator.Send(new CreateOrganizationCommandRequest
        {
            Name = organization.Name,
            Password = organization.Password
        });

        return Ok(mediator);
    }

    [HttpDelete]
    [Route("delete-organization")]
    public virtual async Task<IActionResult> DeleteOrganization([FromQuery] string name)
    {
        var organization = new OrganizationUser(name, string.Empty);

        var mediator = await this.mediator.Send(new DeleteOrganizationCommandRequest
        {
            Name = organization.Name
        });

        return Ok(mediator);
    }

    [HttpPut]
    [Route("update-organization")]
    public virtual async Task<IActionResult> UpdateOrganization([FromBody] OrganizationUser organization)
    {
        var mediator = await this.mediator.Send(new UpdateOrganizationCommandRequest
        {
            Name = organization.Name,
            Password = organization.Password
        });

        return Ok(mediator);
    }

    [HttpPost]
    [Route("active-organization")]
    public virtual async Task<IActionResult> ActiveOrganization([FromQuery] string name)
    {
        var organization = new OrganizationUser(name, string.Empty);

        var mediator = await this.mediator.Send(new ActiveOrganizationCommandRequest
        {
            Name = organization.Name
        });

        return Ok(mediator);
    }

    [HttpGet]
    [Route("map-organization")]
    public virtual async Task<IActionResult> GetOrganization()
    {
        var mediator = await this.mediator.Send(new QueryOrganizationCommandRequest());

        return Ok(mediator);
    }
}