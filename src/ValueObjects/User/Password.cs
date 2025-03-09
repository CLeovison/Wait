
using System.Text.RegularExpressions;
using Wait.Primitives;

namespace Wait.ValueObjects.User;


public class Password : ValueObject
{
    protected static readonly Regex PasswordRegex = new("$(?=.*[A-Z].*[A-Z])(?=.*[!@$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z])^", RegexOptions.Compiled);


    public Password(string value)
    {
        if (PasswordRegex.IsMatch(value))
        {
            throw new ArgumentException("Your Password Must Contain 8 Characters Length, 2 Letters in Uppercase, 1 Special Character, 2 Numerical Symbols and 3 letter lowercase");
        }
        Value = value;
    }
    public string Value { get; init; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}