using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.DeleteOrder;


namespace Ordering.Application.Orders.EventHandlers.Integration;
public class PaymentDeletedEventHandler
    (ISender sender, ILogger<PaymentDeletedEventHandler> logger)
    : IConsumer<PaymentDeletedEvent>
{
    public async Task Consume(ConsumeContext<PaymentDeletedEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var command = MapToDeleteOrderCommand(context.Message);
        await sender.Send(command);
    }

    private DeleteOrderCommand MapToDeleteOrderCommand(PaymentDeletedEvent eventMessage) 
    {
        return new DeleteOrderCommand
        (
            eventMessage.OrderId
        );
    }
}
