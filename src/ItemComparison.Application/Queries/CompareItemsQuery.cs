namespace ItemComparison.Application.Queries;

using ItemComparison.Application.Dtos;
using ItemComparison.Application.Strategy;
using ItemComparison.Domain.Ports;
using ItemComparison.Domain.ValueObjects;

public sealed class CompareItemsQuery
{
    private readonly IProductRepository _repo;
    public CompareItemsQuery(IProductRepository repo) => _repo = repo;

    public async Task<ComparisonResponseDto> ExecuteAsync(IEnumerable<string> rawIds, string? sort, CancellationToken ct)
    {
        var ids = rawIds.Select(id => new ProductId(id)).ToList();
        if (ids.Count == 0) throw new ArgumentException("Pelo menos um Id é obrigatório.");

        var found = await _repo.GetByIdsAsync(ids, ct);
        var foundMap = found.ToDictionary(p => p.Id.Value, p => p, StringComparer.OrdinalIgnoreCase);

        var notFound = ids.Select(i => i.Value).Where(id => !foundMap.ContainsKey(id)).ToList();
        var dtos = found.Select(p => new ProductDto(
            p.Id.Value, p.Name, p.ImageUrl, p.Description, p.Price, p.Rating,
            p.Specs.ToDictionary(kv => kv.Key, kv => kv.Value)
        )).ToList();

        var strategy = ComparisonSortStrategyFactory.Create(sort);
        var ordered = strategy.Sort(dtos);
        return new ComparisonResponseDto(ordered, notFound);
    }
}