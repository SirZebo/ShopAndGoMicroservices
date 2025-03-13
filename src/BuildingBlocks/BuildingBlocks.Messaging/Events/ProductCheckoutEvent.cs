namespace BuildingBlocks.Messaging.Events;
public record ProductCheckoutEvent : IntegrationEvent
{
    public string UserName { get; set; } = default!;
    public Guid CustomerId { get; set; } = default!;
    public Guid MerchantId { get; set; } = default!;
    public Guid ProductId { get; set; } = default!;
    public Guid TransactionToken { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; } = default!;
    public decimal TotalPrice { get; set; } = default!;
    public TimeSpan MaxCompletionTime { get; set; } = default!;
    public string Language { get; set; } = default!;

    // ShippingAddress
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public string AddressLine { get; set; } = default!;
    public string Country { get; set; } = default!;
    public string State { get; set; } = default!;
    public string ZipCode { get; set; } = default!;
}
