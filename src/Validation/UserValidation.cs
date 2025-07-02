
using FluentValidation;
using Wait.Contracts.Data;


namespace Wait.Validation;


public class UserValidation : AbstractValidator<UserDto>
{
    public UserValidation()
    {
        RuleFor(user => user.FirstName).NotEmpty().WithMessage("Please Fill the First Name");

        RuleFor(user => user.LastName).NotEmpty().WithMessage("Please Fill the Last Name");

        RuleFor(user => user.Username).MinimumLength(5).NotEmpty().WithMessage("The Minimum Length for Username is 5 Characters");

        RuleFor(user => user.Password)
        .NotEmpty().Must(p => !string.IsNullOrWhiteSpace(p)).WithMessage("Please Fill the Password")
        .MinimumLength(8).WithMessage("Minimum length is 8 characters")
        .Matches(@"[A-Z]+").WithMessage("Must contain uppercase")
        .Matches(@"[a-z]+").WithMessage("Must contain lowercase")
        .Matches(@"[0-9]+").WithMessage("Must contain number");



        RuleFor(user => user.Email).EmailAddress().NotEmpty().WithMessage("Please provide an email address");
    }
}