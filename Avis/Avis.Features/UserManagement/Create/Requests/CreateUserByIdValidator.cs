using FluentValidation;

namespace Avis.Features.UserManagement.Create.Requests;
public class CreateUserByIdValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserByIdValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required")
                                .MinimumLength(8).WithMessage("Username must be at least 8 characters")
                                .MaximumLength(16).WithMessage("Username must not exceed 16 characters");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                                .MaximumLength(60).WithMessage("Password must not exceed 60 characters")
                                .Must(EvaluatePassword).WithMessage("Password must be strong");
    }
    
    /// <summary>
    /// Password strength check using Zxcvbn library
    /// </summary>
    /// 0 Too guessable: risky password	< 10^3
    /// 1 Very guessable: protection from throttled online attacks	10^3 < guesses < 10^6
    /// 2 Somewhat guessable: protection from unthrottled online attacks 10^6 < guesses < 10^8
    /// 3 Safely unguessable: moderate protection from offline slow-hash scenario 10^8 < guesses < 10^10
    /// 4 Very unguessable: strong protection from offline slow-hash scenario 10^10 < guesses
    /// <returns></returns> 
    private bool EvaluatePassword(string password)
    {
        var strengthResult = Zxcvbn.Core.EvaluatePassword(password);
        return strengthResult.Score >= 3;
    }
}
