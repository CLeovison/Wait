using System.Text.RegularExpressions;
using Wait.Primitives;

namespace Wait.Domain.UserDomain.Common;

public class Username : ValueObject
{
    private static readonly Regex RegexValue = new("^(?=[a-zA-Z0-9._]{8,20}$)(?!.*[_.]{2})[^_.].*[^_.]$", RegexOptions.Compiled);

    public Username(string value)
    {
        if (!RegexValue.IsMatch(value))
        {
            throw new ArgumentException();
        }

        Value = value;
    }
    public string Value { get; init; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}