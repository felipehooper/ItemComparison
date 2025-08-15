namespace ItemComparison.Domain.ValueObjects;

public readonly struct ProductId : IEquatable<ProductId>
{
    public string Value { get; }
    public ProductId(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Id do Produto é Obrigatório");
        foreach (var ch in value) if (!(char.IsLetterOrDigit(ch) || ch is '-' or '_')) throw new ArgumentException("Id inválido");
        Value = value;
    }
    public override string ToString() => Value;
    public bool Equals(ProductId other) => StringComparer.OrdinalIgnoreCase.Equals(Value, other.Value);
    public override bool Equals(object? obj) => obj is ProductId o && Equals(o);
    public override int GetHashCode() => StringComparer.OrdinalIgnoreCase.GetHashCode(Value);
    public static implicit operator string(ProductId id) => id.Value;
}
