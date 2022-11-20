using Avis.Features.Features.Commands.UserOrganizationCommand.ActiveOrganization;
using Avis.Features.Features.Commands.UserOrganizationCommand.CreateOrganization;
using Avis.Features.Features.Commands.UserOrganizationCommand.DeleteOrganization;
using Avis.Features.Features.Commands.UserOrganizationCommand.UpdateOrganization;
using Avis.Services.DataConsultion.Consultion;
using Avis.Services.OrganizationModel;
using MediatR;

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
        #pragma warning disable CS8601 
        var mediator = await this.mediator.Send(new CreateOrganizationCommandRequest
        {
            Name = organization.Name,
            Password = organization.Password
        });
        #pragma warning restore CS8601 

        return Ok(mediator);
    }

    [HttpDelete]
    [Route("delete-organization")]
    public virtual async Task<IActionResult> DeleteOrganization([FromQuery] string name)
    {
        var organization = new OrganizationUser(name, string.Empty);

        #pragma warning disable CS8601 
        var mediator = await this.mediator.Send(new DeleteOrganizationCommandRequest
        {
            Name = organization.Name
        });
        #pragma warning restore CS8601 

        return Ok(mediator);
    }

    [HttpPut]
    [Route("update-organization")]
    public virtual async Task<IActionResult> UpdateOrganization([FromBody] OrganizationUser organization)
    {
        #pragma warning disable CS8601
        var mediator = await this.mediator.Send(new UpdateOrganizationCommandRequest
        {
            Name = organization.Name,
            Password = organization.Password
        });
        #pragma warning restore CS8601
        
        return Ok(mediator);
    }

    [HttpPost]
    [Route("active-organization")]
    public virtual async Task<IActionResult> ActiveOrganization([FromQuery] string name)
    {
        var organization = new OrganizationUser(name, string.Empty);

        #pragma warning disable CS8601
        var mediator = await this.mediator.Send(new ActiveOrganizationCommandRequest
        {
            Name = organization.Name
        });
        #pragma warning restore CS8601

        return Ok(mediator);
    }
}