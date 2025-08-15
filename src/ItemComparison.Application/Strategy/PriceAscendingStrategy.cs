namespace ItemComparison.Application.Strategy;

using ItemComparison.Application.Abstractions;
using ItemComparison.Application.Dtos;

public sealed class PriceAscendingStrategy : IComparisonSortStrategy
{
    public string Name => "price-asc";
    public IReadOnlyList<ProductDto> Sort(IReadOnlyList<ProductDto> items)
        => items.OrderBy(p => p.Price).ThenBy(p => p.Name, StringComparer.OrdinalIgnoreCase).ToList();
}