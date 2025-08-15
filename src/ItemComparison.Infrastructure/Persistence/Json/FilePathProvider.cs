namespace ItemComparison.Infrastructure.Persistence.Json;

using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;
using ItemComparison.Domain.Entities;
using ItemComparison.Domain.Ports;
using ItemComparison.Domain.ValueObjects;

public sealed class FilePathProvider
{
    public string DataDirectory { get; }
    public string ItemsPath => Path.Combine(DataDirectory, "items.json");
    public FilePathProvider(string? baseDir = null)
    {
        baseDir ??= AppContext.BaseDirectory;
        DataDirectory = Path.Combine(baseDir, "data");
        Directory.CreateDirectory(DataDirectory);
    }
}

public sealed class JsonProductRepository : IProductRepository
{
    private readonly FilePathProvider _paths;
    private readonly ConcurrentDictionary<string, Product> _cache = new(StringComparer.OrdinalIgnoreCase);
    private readonly JsonSerializerOptions _json = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    private volatile bool _initialized;

    public JsonProductRepository(FilePathProvider paths) => _paths = paths;

    private async Task EnsureInitAsync(CancellationToken ct)
    {
        if (_initialized) return;
        if (!File.Exists(_paths.ItemsPath)) await File.WriteAllTextAsync(_paths.ItemsPath, "[]", ct);
        await using var fs = File.OpenRead(_paths.ItemsPath);
        var list = await JsonSerializer.DeserializeAsync<List<PersistenceModel>>(fs, _json, ct) ?? new();
        foreach (var m in list) _cache[m.Id] = m.ToDomain();
        _initialized = true;
    }

    private async Task PersistAsync(CancellationToken ct)
    {
        var list = _cache.Values
            .OrderBy(p => p.Id.Value, StringComparer.OrdinalIgnoreCase)
            .Select(PersistenceModel.FromDomain).ToList();
        var tmp = _paths.ItemsPath + ".tmp";
        await File.WriteAllTextAsync(tmp, JsonSerializer.Serialize(list, _json), ct);
        File.Move(tmp, _paths.ItemsPath, true);
    }

    public async Task<IReadOnlyCollection<Product>> GetAllAsync(CancellationToken ct)
    {
        await EnsureInitAsync(ct);
        return _cache.Values.ToList().AsReadOnly();
    }

    public async Task<Product?> GetByIdAsync(ProductId id, CancellationToken ct)
    {
        await EnsureInitAsync(ct);
        _cache.TryGetValue(id.Value, out var p);
        return p;
    }

    public async Task<IReadOnlyList<Product>> GetByIdsAsync(IEnumerable<ProductId> ids, CancellationToken ct)
    {
        await EnsureInitAsync(ct);
        var res = new List<Product>();
        foreach (var id in ids) if (_cache.TryGetValue(id.Value, out var p)) res.Add(p);
        return res.AsReadOnly();
    }

    public async Task UpsertAsync(Product product, CancellationToken ct)
    {
        await EnsureInitAsync(ct);
        _cache[product.Id.Value] = product;
        await PersistAsync(ct);
    }

    public async Task<bool> DeleteAsync(ProductId id, CancellationToken ct)
    {
        await EnsureInitAsync(ct);
        var removed = _cache.TryRemove(id.Value, out _);
        if (removed) await PersistAsync(ct);
        return removed;
    }

    private sealed class PersistenceModel
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public double Rating { get; set; }
        public Dictionary<string, string>? Specs { get; set; }

        public static PersistenceModel FromDomain(Product p) => new()
        {
            Id = p.Id.Value, Name = p.Name, ImageUrl = p.ImageUrl, Description = p.Description,
            Price = p.Price, Rating = p.Rating, Specs = p.Specs.ToDictionary(x => x.Key, x => x.Value)
        };

        public Product ToDomain() => new(new ProductId(Id), Name, ImageUrl, Description, Price, Rating);
    }
}