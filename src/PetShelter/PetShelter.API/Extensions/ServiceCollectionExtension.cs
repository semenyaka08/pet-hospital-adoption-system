using MassTransit;
using PetShelter.Infrastructure.EventConsumers;

namespace PetShelter.API.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddSwaggerGen();

        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();

            config.AddConsumer<PetTransferredToShelterEventConsumer>();
            
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

        services.AddGrpc();
        
        return services;
    }
}