using FluentValidation;

namespace Avis.Features.UserManagement.Auth.Requests;

public class AuthUserByIdValidator : AbstractValidator<AuthUserRequest>
{
    public AuthUserByIdValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required")
                                .MinimumLength(8).WithMessage("Username must be at least 8 characters");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                                .MinimumLength(8).WithMessage("Password must be at least 8 characters");
    }
}

