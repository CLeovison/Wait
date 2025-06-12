using Microsoft.EntityFrameworkCore;


namespace Wait.Domain.Entities;

/// <summary>
/// This Class Is The Entity Based Class of the User
/// </summary>
[PrimaryKey(nameof(UserId))]
public sealed class Users
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateOnly Birthday { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}