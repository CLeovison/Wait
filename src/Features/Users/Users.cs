using Wait.Domain.Common;

namespace Wait.Features.Users;

public sealed class Users : AuditableEntity
{


    public Guid UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsVerifiedEmail { get; set; }
    public DateOnly Birthday { get; set; }
}