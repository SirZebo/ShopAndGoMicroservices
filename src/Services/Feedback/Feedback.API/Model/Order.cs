﻿namespace Feedback.API.Model;

public class Order
{
    public Guid Id { get; set; } = default!;
    public Guid CustomerId { get; set; } = default!;
    public Guid MerchantId { get; set; } = default!;
    public Guid ProductId { get; set; } = default!;
    public int Quantity { get; set; } = default!;
    public DateTime OrderDeadline { get; set; }
}
