using BuildingBlocks.Messaging.Events;
using Finance.API.Dtos;
using MassTransit;

namespace Finance.API.Payments.CreatePayment;

public class OrderCreatedEventHandler
    (ISender sender, ILogger<OrderCreatedEventHandler> logger)
    : IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
        var payment = MapToPaymentDto(context);
        var command = new CreatePaymentCommand(payment);

        await sender.Send(command);
    }

    private static PaymentDto MapToPaymentDto(ConsumeContext<OrderCreatedEvent> context)
    {
        return new PaymentDto
        {
            Id = Guid.Empty,
            CustomerId = context.Message.CustomerId,
            TransactionToken = context.Message.TransactionToken,
            TotalPrice = context.Message.TotalPrice
        };
    }

}
