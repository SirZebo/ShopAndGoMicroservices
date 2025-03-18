using BuildingBlocks.Messaging.Events.Payments;
using Finance.API.Payments.RefundPayment;
using Finance.API.Payments.ReleasePayment;
using MassTransit;

namespace Finance.API.Payments.EventHandler.Integration;

public class OrderCancelledEventHandler
    (ISender sender,
    ILogger<OrderCancelledEventHandler> logger)
    : IConsumer<OrderCancelledEvent>
{
    public async Task Consume(ConsumeContext<OrderCancelledEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
        var command = MapToRefundPaymentCommand(context.Message);
        await sender.Send(command);
    }

    private RefundPaymentCommand MapToRefundPaymentCommand(OrderCancelledEvent message)
    {
        return new RefundPaymentCommand(
            message.OrderId,
            message.TotalPrice
        );

    }
}