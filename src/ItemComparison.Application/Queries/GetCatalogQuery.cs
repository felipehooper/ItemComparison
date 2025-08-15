using ItemComparison.Application.Dtos;
using ItemComparison.Domain.Ports;

public sealed class GetCatalogQuery
{
    private readonly IProductRepository _repo;
    public GetCatalogQuery(IProductRepository repo) => _repo = repo;

    public async Task<IReadOnlyList<ProductDto>> ExecuteAsync(CancellationToken ct)
    {
        var all = await _repo.GetAllAsync(ct);
        return all.Select(p => new ProductDto(
            p.Id.Value, p.Name, p.ImageUrl, p.Description, p.Price, p.Rating,
            p.Specs.ToDictionary(kv => kv.Key, kv => kv.Value)
        )).OrderBy(p => p.Name, StringComparer.OrdinalIgnoreCase).ToList();
    }
}