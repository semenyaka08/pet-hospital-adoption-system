using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pet.Infrastructure.Interceptors;

namespace Pet.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        serviceCollection.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        serviceCollection.AddScoped<ISaveChangesInterceptor, DomainEventDispatcherInterceptor>();
        
        serviceCollection.AddDbContext<PetDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetService<ISaveChangesInterceptor>()!);
            options.UseSqlServer(configuration.GetConnectionString(connectionString!));
        });
        
        return serviceCollection;
    }
}