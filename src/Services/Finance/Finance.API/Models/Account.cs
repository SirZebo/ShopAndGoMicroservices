using Marten.Schema;

namespace Finance.API.Models;

[UseOptimisticConcurrency]
public class Account
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; } = 0;
}
