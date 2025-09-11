
using Wait.Domain.Common;

namespace Wait.Domain.Entities;

/// <summary>
/// This Class Is The Entity Based Class of the User
/// </summary>

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