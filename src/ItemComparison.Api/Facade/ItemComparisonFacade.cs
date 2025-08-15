using ItemComparison.Application.Commands;
using ItemComparison.Application.Dtos;
using ItemComparison.Application.Queries;

public sealed class ItemComparisonFacade
{
    private readonly CompareItemsQuery _compare;
    private readonly GetCatalogQuery _catalog;
    private readonly GetItemByIdQuery _getById;
    private readonly UpsertItemCommand _upsert;
    private readonly DeleteItemCommand _delete;

    public ItemComparisonFacade(CompareItemsQuery compare, GetCatalogQuery catalog, GetItemByIdQuery getById,
        UpsertItemCommand upsert, DeleteItemCommand delete)
    {
        _compare = compare; _catalog = catalog; _getById = getById; _upsert = upsert; _delete = delete;
    }

    public Task<ComparisonResponseDto> CompareAsync(IEnumerable<string> ids, string? sort, CancellationToken ct)
        => _compare.ExecuteAsync(ids, sort, ct);

    public Task<IReadOnlyList<ProductDto>> CatalogAsync(CancellationToken ct)
        => _catalog.ExecuteAsync(ct);

    public Task<ProductDto?> GetByIdAsync(string id, CancellationToken ct)
        => _getById.ExecuteAsync(id, ct);

    public Task<ProductDto> UpsertAsync(ProductDto dto, CancellationToken ct)
        => _upsert.ExecuteAsync(dto, ct);

    public Task<bool> DeleteAsync(string id, CancellationToken ct)
        => _delete.ExecuteAsync(id, ct);
}