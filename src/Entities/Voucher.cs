namespace Wait.Domain.Entities;


public sealed class Voucher
{
    public Guid VoucherId { get; set; } = Guid.CreateVersion7();
    public string? VoucherCode { get; set; }
    public decimal Amount { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsRedeemed { get; set; }
    public string? Description { get; set; }
}
