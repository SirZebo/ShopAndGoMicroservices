namespace Finance.API.Models;

public class Account
{
    public Guid CustomerId {  get; set; }
    public decimal Balance { get; set; } = 0;
}
