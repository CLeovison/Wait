using Wait.Domain.UserDomain.Common;

namespace Wait.Domain.UserDomain;


public class User
{
    public required Username Username { get; init; }
    public required Password Password { get; init; }
    public required FirstName FirstName { get; init; }
    public required LastName LastName { get; init; }
    public required Email Email { get; init; }
}