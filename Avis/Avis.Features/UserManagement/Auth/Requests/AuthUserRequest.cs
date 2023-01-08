namespace Avis.Features.UserManagement.Auth.Requests;
public class AuthUserRequest : MediatR.IRequest<AuthUserResponse>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}