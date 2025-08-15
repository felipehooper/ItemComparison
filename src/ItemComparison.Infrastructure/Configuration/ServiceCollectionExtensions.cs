using ItemComparison.Domain.Ports;
using ItemComparison.Infrastructure.Persistence;
using ItemComparison.Infrastructure.Persistence.Json;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<FilePathProvider>();
        services.AddSingleton<IProductRepository>(sp =>
            StorageFactory.CreateJsonRepository(sp.GetRequiredService<FilePathProvider>()));
        return services;
    }
}