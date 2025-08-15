namespace ItemComparison.Application.Commands;

using ItemComparison.Application.Dtos;
using ItemComparison.Domain.Entities;
using ItemComparison.Domain.Ports;
using ItemComparison.Domain.ValueObjects;

public sealed class UpsertItemCommand
{
    private readonly IProductRepository _repo;
    public UpsertItemCommand(IProductRepository repo) => _repo = repo;

    public async Task<ProductDto> ExecuteAsync(ProductDto dto, CancellationToken ct)
    {
        var entity = new Product(new ProductId(dto.Id), dto.Name, dto.ImageUrl, dto.Description, dto.Price, dto.Rating);
        await _repo.UpsertAsync(entity, ct);
        return dto with { Specs = entity.Specs.ToDictionary(kv => kv.Key, kv => kv.Value) };
    }
}