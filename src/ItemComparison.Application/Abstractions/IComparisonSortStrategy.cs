namespace ItemComparison.Application.Abstractions;

using ItemComparison.Application.Dtos;

public interface IComparisonSortStrategy
{
    IReadOnlyList<ProductDto> Sort(IReadOnlyList<ProductDto> items);
    string Name { get; }
}