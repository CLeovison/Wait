
using FluentValidation;
using Wait.Domain.Entities;

namespace Wait.Validation;


public class UserValidation : AbstractValidator<Users>
{
    public UserValidation()
    {
        RuleFor(user => user.FirstName).NotEmpty().WithMessage("Please Fill the First Name");

        RuleFor(user => user.LastName).NotEmpty().WithMessage("Please Fill the Last Name");

        RuleFor(user => user.Username).MinimumLength(5).NotEmpty().WithMessage("The Minimum Length for Username is 5 Characters");

        RuleFor(user => user.Password).NotEmpty().MinimumLength(8)
        .WithMessage("Your Password length must be atleast 8")
        .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
        .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
        .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
   
        
        RuleFor(user => user.Email).EmailAddress().NotEmpty().WithMessage("Please provide an email address");
    }
}