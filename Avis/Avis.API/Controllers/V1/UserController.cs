using Avis.Features.UserManagement.Create.Requests;
using Avis.Services.Models;

namespace Avis.API.Controllers.V1;

[Area("service")]
[Route("api/v{version:apiVersion}/service")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpPost]
    [Route("create-user")]
    public virtual async Task<IActionResult> CreateUser([FromBody] DerivedUserVirtualClass user)
    {
        var mediator = await this._mediator.Send(new CreateUserRequest
        {
            UserName = user.UserName,
            Password = user.Password
        });

        return Ok(mediator);
    }
}