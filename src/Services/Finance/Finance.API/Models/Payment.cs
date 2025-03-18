namespace Finance.API.Models;

public class Payment
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid MerchantId { get; set; }
    public Guid OrderId { get; set; }
    public Guid TransactionToken { get; set; }
    public decimal OutstandingAmount { get; set; }
    public decimal TotalPrice { get; set; }
}
