using BuildingBlocks.Messaging.Events;
using MassTransit;
using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.EventHandlers.Domain;
public class OrderCreatedEventHandler
    (IPublishEndpoint publishEndpoint,
    IFeatureManager featureManager,
    ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        //var orderCreatedIntegrationEvent = domainEvent.Order.ToOrderDto();

        //await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
        var paymentStartedIntegrationEvent = new PaymentStartedEvent
        {
            OrderId = domainEvent.Order.Id.Value,
            TotalPrice = domainEvent.Order.TotalPrice
        };

        await publishEndpoint.Publish(paymentStartedIntegrationEvent, cancellationToken);
    }
}
