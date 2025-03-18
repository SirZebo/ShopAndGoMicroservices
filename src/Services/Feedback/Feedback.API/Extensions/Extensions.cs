using Feedback.API.Reviews.EventHandler.Integration;
using MassTransit;
using System.Reflection;

namespace Feedback.API.Extensions;

public static class Extensions
{
    private static string instanceId = "Feedback";
    public static IServiceCollection AddMessageBroker
        (this IServiceCollection services,
        IConfiguration configuration,
        Assembly? assembly = null)
    {

        services.AddMassTransit(config =>
        {
            // Set naming convention for endpoints
            config.SetKebabCaseEndpointNameFormatter();

            config.AddConsumer<ShipmentDeliveredEventHandler>() // Pub-Sub
                .Endpoint(e => e.InstanceId = instanceId);

            config.UsingRabbitMq((context, configurator) =>
            {
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
