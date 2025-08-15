namespace ItemComparison.Application.Strategy;

using ItemComparison.Application.Abstractions;
using ItemComparison.Application.Dtos;

public sealed class RatingDescendingStrategy : IComparisonSortStrategy
{
    public string Name => "rating-desc";
    public IReadOnlyList<ProductDto> Sort(IReadOnlyList<ProductDto> items)
        => items.OrderByDescending(p => p.Rating).ThenBy(p => p.Price).ToList();
}