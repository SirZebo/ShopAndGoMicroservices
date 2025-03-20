using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace JobScheduler.API.Jobs.EventHandler.Integration;

//public class CommercePayStartedEventHandler
//    (ILogger<PaymentCreatedEventHandler> logger)
//    : IConsumer<CommercePayStartedEvent>
//{
//    public async Task Consume(ConsumeContext<CommercePayStartedEvent> context)
//    {
//        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

//        await Task.CompletedTask;
//    }
//}