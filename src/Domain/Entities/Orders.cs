namespace Wait.Domain.Entities;


//Orders Entity Represents of Number of order that the user has order
public class Orders
{
    public Guid OrderId { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
}