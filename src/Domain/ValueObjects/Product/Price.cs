using Wait.Domain.Primitives;

namespace Wait.ValueObjects.Product;

public class Price : ValueObject
{


    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}