namespace Wait.Domain.Entities;


public sealed class Voucher
{
    public string? VoucherCode { get; set; }
    public decimal Amount { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsRedeemed { get; set; }
    public string? Description { get; set; }
}
