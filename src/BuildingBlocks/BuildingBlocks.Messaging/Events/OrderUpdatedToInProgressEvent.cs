using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Messaging.Events;
public record OrderUpdatedToInProgressEvent : IntegrationEvent
{
    public Guid OrderId { get; set; } = default!;
    public Guid CustomerId { get; set; } = default!;
    public Guid ProductId { get; set; } = default!;
    public int Quantity { get; set; } = default!;
    public TimeSpan MaxCompletionTime { get; set; }
    public int OrderStatus { get; set; } = default!;

    // Shipping and BillingAddress
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public string AddressLine { get; set; } = default!;
    public string Country { get; set; } = default!;
    public string State { get; set; } = default!;
    public string ZipCode { get; set; } = default!;
    
}

