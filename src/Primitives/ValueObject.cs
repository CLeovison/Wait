namespace Wait.Primitives;


public abstract class ValueObject : IEquatable<ValueObject>
{

    protected abstract IEnumerable<object> GetEqualityComponents();

    private bool ValuesAreEqual(ValueObject valueObject) => GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());

    public override int GetHashCode() => GetEqualityComponents().Aggregate(
        default(int), (hashcode, value) 
        => HashCode.Combine(hashcode, value.GetHashCode()));

    public static bool operator ==(ValueObject? a, ValueObject? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    // This line of code/represent that when you declare a non equal operator then the value will be false
    public static bool operator !=(ValueObject? a, ValueObject? b) => !(a == b);

    // The Virtual Keyword Represents as A Parent Class, but if you want a specialized implementation or Value into it 

    public virtual bool Equals(ValueObject? other) => other is not null && ValuesAreEqual(other);

    // That's the override cames in it.
    public override bool Equals(object? obj) => obj is ValueObject valueObject && ValuesAreEqual(valueObject);


}