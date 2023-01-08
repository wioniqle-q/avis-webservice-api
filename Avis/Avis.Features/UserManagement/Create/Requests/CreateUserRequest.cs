using MediatR;

namespace Avis.Features.UserManagement.Create.Requests;
public class CreateUserRequest : IRequest<CreateUserResponse>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
