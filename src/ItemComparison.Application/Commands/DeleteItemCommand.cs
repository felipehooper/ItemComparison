namespace ItemComparison.Application.Commands;

using ItemComparison.Domain.Ports;
using ItemComparison.Domain.ValueObjects;

public sealed class DeleteItemCommand
{
    private readonly IProductRepository _repo;
    public DeleteItemCommand(IProductRepository repo) => _repo = repo;
    public Task<bool> ExecuteAsync(string id, CancellationToken ct) => _repo.DeleteAsync(new ProductId(id), ct);
}