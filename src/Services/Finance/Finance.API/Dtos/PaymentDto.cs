namespace Finance.API.Dtos;

public class PaymentDto
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid TransactionToken { get; set; }
    public decimal TotalPrice { get; set; }
}
