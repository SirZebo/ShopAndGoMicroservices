using BuildingBlocks.Messaging.Events;
using Hangfire;
using JobScheduler.API.Data;
using JobScheduler.API.Models;
using MassTransit;
using MediatR;

namespace JobScheduler.API.Jobs.EventHandler.Integration;

public class PaymentCreatedEventHandler
    (IMessageScheduler scheduler,
    ApplicationDbContext dbContext,
    ILogger<PaymentCreatedEventHandler> logger)
    : IConsumer<PaymentCreatedEvent>
{
    public async Task Consume(ConsumeContext<PaymentCreatedEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var paymentDeadlineExceededEvent = new PaymentDeadlineExceededEvent { PaymentId = context.Message.PaymentId };
        var scheduledMessage = await scheduler.SchedulePublish(DateTime.UtcNow.AddHours(1), paymentDeadlineExceededEvent);

        var payment = new Payment { Id = context.Message.PaymentId, TokenId = scheduledMessage.TokenId };
        await dbContext.Payments.AddAsync(payment);

        await Task.CompletedTask;
    }
}
