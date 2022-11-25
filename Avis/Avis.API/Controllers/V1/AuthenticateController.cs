
using Avis.Features.Features.Commands.AuthenticateCommand.Authenticate;

namespace Avis.API.Controllers.V1;

[Area("authenticate")]
[Route("api/v{version:apiVersion}/authenticate")]
[ApiController]
public class AuthenticateController : ControllerBase
{
    private protected AccountConsultion _accountConsultion { get; }
    private protected IMediator _mediator { get; }

    public AuthenticateController(AccountConsultion accountConsultion, IMediator mediator)
    {
        this._accountConsultion = accountConsultion;
        this._mediator = mediator;
    }

    [HttpPost]
    [Route("login")]
    public virtual async Task<IActionResult> Login([FromBody] OrganizationUser organization)
    {
        var mediator = await this._mediator.Send(new AuthenticateCommandRequest
        {
            Name = organization.Name,
            Password = organization.Password
        });

        return Ok(mediator);
    }
}
