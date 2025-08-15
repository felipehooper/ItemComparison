namespace ItemComparison.Domain.Entities;

using System.ComponentModel.DataAnnotations;
using ItemComparison.Domain.ValueObjects;

public sealed class Product
{
    public ProductId Id { get; }
    public string Name { get; private set; }
    public string? ImageUrl { get; private set; }
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public double Rating { get; private set; }
    private readonly Dictionary<string, string> _specs = new();
    public IReadOnlyDictionary<string, string> Specs => _specs;

    public Product(ProductId id, string name, string? imageUrl, string? description, decimal price, double rating)
    {
        Id = id;
        Name = name;
        ImageUrl = imageUrl;
        Description = description;
        SetPrice(price);
        SetRating(rating);
    }

    public void Rename(string name) => Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException() : name;
    public void SetPrice(decimal price){ if (price < 0) throw new ArgumentOutOfRangeException(); Price = price; }
    public void SetRating(double rating){ if (rating is <0 or >5) throw new ArgumentOutOfRangeException(); Rating = rating; }
}