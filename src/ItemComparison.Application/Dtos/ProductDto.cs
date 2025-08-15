namespace ItemComparison.Application.Dtos;

public sealed record ProductDto(
    string Id,
    string Name,
    string? ImageUrl,
    string? Description,
    decimal Price,
    double Rating,
    Dictionary<string, string>? Specs
);