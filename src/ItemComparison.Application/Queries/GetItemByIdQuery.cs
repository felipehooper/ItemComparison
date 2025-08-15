namespace ItemComparison.Application.Queries;

using ItemComparison.Application.Dtos;
using ItemComparison.Domain.Ports;
using ItemComparison.Domain.ValueObjects;

public sealed class GetItemByIdQuery
{
    private readonly IProductRepository _repo;
    public GetItemByIdQuery(IProductRepository repo) => _repo = repo;

    public async Task<ProductDto?> ExecuteAsync(string id, CancellationToken ct)
    {
        var prod = await _repo.GetByIdAsync(new ProductId(id), ct);
        return prod is null ? null : new ProductDto(
            prod.Id.Value, prod.Name, prod.ImageUrl, prod.Description, prod.Price, prod.Rating,
            prod.Specs.ToDictionary(kv => kv.Key, kv => kv.Value)
        );
    }
}