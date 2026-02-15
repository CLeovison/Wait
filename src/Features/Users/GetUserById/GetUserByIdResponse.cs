using Wait.Domain.Common;

namespace Wait.Features.Users.GetUserById;


public sealed class GetUserByIdResponse : AuditableEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public DateOnly Birthday { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool IsVerifiedEmail { get; set; }
};