using FluentValidation;
using Wait.Contracts.Request.UserRequest;

namespace Wait.Validation;

public sealed class UpdateUserValidation : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidation()
    {

    }
}