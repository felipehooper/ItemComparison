namespace ItemComparison.Domain.Ports;

using ItemComparison.Domain.Entities;
using ItemComparison.Domain.ValueObjects;

public interface IProductRepository
{
    Task<IReadOnlyCollection<Product>> GetAllAsync(CancellationToken ct);
    Task<Product?> GetByIdAsync(ProductId id, CancellationToken ct);
    Task<IReadOnlyList<Product>> GetByIdsAsync(IEnumerable<ProductId> ids, CancellationToken ct);
    Task UpsertAsync(Product product, CancellationToken ct);
    Task<bool> DeleteAsync(ProductId id, CancellationToken ct);
}