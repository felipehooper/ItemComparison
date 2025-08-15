namespace ItemComparison.Shared.Errors;

public sealed record ErrorResponse(string Code, string Message, object? Details = null);