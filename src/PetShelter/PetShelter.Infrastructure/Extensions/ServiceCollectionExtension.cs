using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetShelter.Domain.Repositories;
using PetShelter.Infrastructure.Interceptors;
using PetShelter.Infrastructure.Repositories;

namespace PetShelter.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PetDb");
        
        serviceCollection.AddScoped<IPetRepository, PetRepository>();
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        serviceCollection.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        serviceCollection.AddScoped<ISaveChangesInterceptor, DomainEventDispatcherInterceptor>();
        
        serviceCollection.AddDbContext<PetDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetService<ISaveChangesInterceptor>()!);
            options.UseSqlServer(connectionString);
        });
        
        return serviceCollection;
    }
}