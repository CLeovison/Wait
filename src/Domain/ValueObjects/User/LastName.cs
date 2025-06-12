using System.Text.RegularExpressions;
using  Wait.Domain.Primitives;

namespace Wait.ValueObjects.User;

public class LastName : ValueObject
{
    private static readonly Regex LastNameRegex = new("$[a-z ,.'-]+^", RegexOptions.Compiled);
    public LastName(string value)
    {
        if (!LastNameRegex.IsMatch(value))
        {
            throw new ArgumentException("Name must not contain special characters");
        }

        Value = value;
    }

    public string Value { get; init; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }


}