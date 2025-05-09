﻿using BuildingBlocks.Messaging.Events;
using MassTransit;
using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.EventHandlers.Domain;
public class OrderCreatedEventHandler
    (IPublishEndpoint publishEndpoint,
    ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<Ordering.Domain.Events.OrderCreatedEvent>
{
    public async Task Handle(Ordering.Domain.Events.OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        var orderDto = domainEvent.Order.ToOrderDto();
        var orderCreatedIntegrationEvent = MapToOrderCreatedIntegrationEvent(orderDto);

        await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
    }

    public BuildingBlocks.Messaging.Events.OrderCreatedEvent MapToOrderCreatedIntegrationEvent(OrderDto orderDto)
    {
        return new BuildingBlocks.Messaging.Events.OrderCreatedEvent
        {
            OrderId = orderDto.Id,
            CustomerId = orderDto.CustomerId,
            TransactionToken = orderDto.TransactionToken,
            TotalPrice = orderDto.TotalPrice,
            MaxCompletionTime = orderDto.MaxCompletionTime
        };
    }
}