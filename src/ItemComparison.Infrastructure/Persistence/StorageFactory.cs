namespace ItemComparison.Infrastructure.Persistence;

using ItemComparison.Domain.Ports;
using ItemComparison.Infrastructure.Persistence.Json;

public static class StorageFactory
{
    public static IProductRepository CreateJsonRepository(FilePathProvider provider)
        => new JsonProductRepository(provider);
}