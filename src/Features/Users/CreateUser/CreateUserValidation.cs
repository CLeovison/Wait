using FluentValidation;
using Wait.Features.Users.Create;

namespace Wait.Features.Users.CreateUser;

public sealed class CreateUserValidation : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidation()
    {
        RuleFor(user => user.FirstName).NotEmpty().WithMessage("Please Fill the First Name");

        RuleFor(user => user.LastName).NotEmpty().WithMessage("Please Fill the Last Name");

        RuleFor(user => user.Username).MinimumLength(5).NotEmpty().WithMessage("The Minimum Length for Username is 5 Characters");

        RuleFor(user => user.Password)
        .NotEmpty().Must(p => !string.IsNullOrWhiteSpace(p)).WithMessage("Please Fill the Password")
        .MinimumLength(8).WithMessage("Minimum length is 8 characters")
        .Matches(@"[A-Z]+").WithMessage("Password must contain uppercase")
        .Matches(@"[a-z]+").WithMessage("Password must contain lowercase")
        .Matches(@"[0-9]+").WithMessage("Password must contain number");

        RuleFor(user => user.ConfirmPassowrd).Matches(user => user.Password).WithMessage("Your confirmation password must match the password you entered.");
        
        RuleFor(user => user.Email).EmailAddress().NotEmpty().WithMessage("Please provide an email address");
    }
}