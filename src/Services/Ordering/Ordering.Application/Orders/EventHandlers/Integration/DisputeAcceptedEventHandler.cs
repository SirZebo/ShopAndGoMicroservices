using BuildingBlocks.Messaging.Events.Disputes;
using BuildingBlocks.Messaging.Events.Review;
using MassTransit;
using Ordering.Application.Orders.Commands.UpdateOrderStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.EventHandlers.Integration;
public class DisputeAcceptedEventHandler
    (ISender sender, ILogger<DisputeAcceptedEventHandler> logger)
    : IConsumer<DisputeAcceptedEvent>
{
    public async Task Consume(ConsumeContext<DisputeAcceptedEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var command = MapToUpdateOrderStatusCommand(context.Message);
        await sender.Send(command);
    }

    private UpdateOrderStatusCommand MapToUpdateOrderStatusCommand(DisputeAcceptedEvent message)
    {
        var orderDto = new OrderDto
            (
                Id: message.OrderId,
                CustomerId: Guid.Empty,
                MerchantId: Guid.Empty,
                TransactionToken: Guid.Empty,
                OrderName: null,
                ShippingAddress: null,
                OrderItems: null,
                MaxCompletionTime: TimeSpan.Zero,
                Status: Ordering.Domain.Enums.OrderStatus.Cancelled,
                TotalPrice: 0
            );
        return new UpdateOrderStatusCommand(orderDto);
    }
}