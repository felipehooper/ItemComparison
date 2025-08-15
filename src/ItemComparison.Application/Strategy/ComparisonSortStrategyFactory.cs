namespace ItemComparison.Application.Strategy;

using ItemComparison.Application.Abstractions;

public static class ComparisonSortStrategyFactory
{
    public static IComparisonSortStrategy Create(string? key)
        => key?.ToLowerInvariant() switch
        {
            "price-asc" => new PriceAscendingStrategy(),
            "rating-desc" => new RatingDescendingStrategy(),
            _ => new RatingDescendingStrategy(),
        };
}