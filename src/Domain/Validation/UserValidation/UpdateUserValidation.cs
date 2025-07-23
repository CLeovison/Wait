using FluentValidation;
using Wait.Contracts.Request.UserRequest;

namespace Wait.Domain.Validation.UserValidation;
public sealed class UpdateUserValidation : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidation()
    {

    }
}