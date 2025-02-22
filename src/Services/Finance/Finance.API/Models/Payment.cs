namespace Finance.API.Models;

public class Payment
{
    public Guid Id { get; set; }
    public Guid TransactionToken { get; set; }
    public decimal TotalPrice { get; set; }
}
