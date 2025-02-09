using System.Text.RegularExpressions;
using Wait.Primitives;

namespace Wait.Domain.UserDomain.Common;

public class Username : ValueObject
{

    public string Value { get; init; }
    public Regex GetRegex { get; init; }
    public Username(Regex regex, string value)
    {

    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}