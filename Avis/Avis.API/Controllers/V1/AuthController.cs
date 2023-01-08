using Avis.Features.UserManagement.Auth.Requests;
using Avis.Services.DataConsultion.Consultion;
using Avis.Services.Models;

namespace Avis.API.Controllers.V1;

[Area("auth")]
[Route("api/v{version:apiVersion}/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly IMediator _mediator;

    public AuthController(AuthService authService, IMediator mediator)
    {
        this._authService = authService;
        this._mediator = mediator;
    }

    [HttpPost]
    [Route("verify")]
    public virtual async Task<IActionResult> Login([FromBody] DerivedUserVirtualClass userModel)
    {
        var mediator = await this._mediator.Send(new AuthUserRequest
        {
            UserName = userModel.UserName,
            Password = userModel.Password
        });

        return Ok(mediator);        
    }
}
