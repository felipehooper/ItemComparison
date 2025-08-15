namespace ItemComparison.Application.Dtos;

public sealed record ComparisonResponseDto(
    IReadOnlyList<ProductDto> Items,
    IReadOnlyList<string> NotFoundIds
);