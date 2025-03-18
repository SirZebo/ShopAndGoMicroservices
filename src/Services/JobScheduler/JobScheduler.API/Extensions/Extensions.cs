using BuildingBlocks.Messaging.Events;
using JobScheduler.API.Jobs.EventHandler.Integration;
using MassTransit;
using System.Reflection;

namespace JobScheduler.API.Extensions;

public static class Extensions
{
    private static string instanceId = "JobScheduler";
    public static IServiceCollection AddMessageBroker
        (this IServiceCollection services,
        IConfiguration configuration,
        Assembly? assembly = null)
    {

        services.AddMassTransit(config =>
        {
            // Set naming convention for endpoints
            config.SetKebabCaseEndpointNameFormatter();

            config.AddPublishMessageScheduler();

            config.AddConsumer<PaymentCreatedEventHandler>()
                .Endpoint(e => e.InstanceId = instanceId); // Pub-Sub

            config.AddConsumer<ReviewCreatedEventHandler>() // Pub-Sub
                .Endpoint(e => e.InstanceId = instanceId);
             
            config.AddConsumer<ShipmentCreatedEventHandler>() // Pub-Sub
                .Endpoint(e => e.InstanceId = instanceId);

            config.AddConsumer<ShipmentDeliveredEventHandler>() // Pub-Sub
                .Endpoint(e => e.InstanceId = instanceId);

            config.AddConsumer<ReviewUpdatedEventHandler>() // Pub-Sub
                .Endpoint(e => e.InstanceId = instanceId);

            config.UsingRabbitMq((context, configurator) =>
            {
                configurator.UsePublishMessageScheduler();
                configurator.UseHangfireScheduler();
                configurator.Host(new Uri(configuration["MessageBroker:Host"]!), host =>
                {
                    host.Username(configuration["MessageBroker:UserName"]);
                    host.Password(configuration["MessageBroker:Password"]);
                });
                configurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}