
using System.Text.RegularExpressions;
using Wait.Primitives;

namespace Wait.Domain.UserDomain.Common;

public class FirstName : ValueObject
{
    private static readonly Regex NameRegex = new("^[a-z ,.'-]+$");

    public FirstName(string value)
    {
        if (!NameRegex.IsMatch(value))
        {
            throw new ArgumentException("The Name that you input was not a valid name");
        }

        if (value is null)
        {
            throw new ArgumentException("The Firstname must not be empty");
        }

        Value = value;
    }


    public string Value { get; init; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}