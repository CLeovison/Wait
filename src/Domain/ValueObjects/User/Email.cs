using System.Text.RegularExpressions;
using Wait.Domain.Primitives;

namespace Wait.ValueObjects.User;


public class Email : ValueObject
{

    private static readonly Regex EmailRegex = new("^[a-zA-Z0-9._%±]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$");
    public Email(string value)
    {
        if (!EmailRegex.IsMatch(value))
        {
            throw new ArgumentException("Your email address is not valid");
        }

        Value = value;
    }

    public string Value { get; init; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new ArgumentException();
    }
}