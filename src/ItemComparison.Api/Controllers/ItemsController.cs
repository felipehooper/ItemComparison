using ItemComparison.Api.Facade;
using ItemComparison.Application.Dtos;
using ItemComparison.Shared.Errors;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
public sealed class ItemsController : ControllerBase
{
    private readonly ItemComparisonFacade _facade;
    public ItemsController(ItemComparisonFacade facade) => _facade = facade;

    [HttpGet("items")]
    [ProducesResponseType(typeof(ComparisonResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Compare([FromQuery] string? ids, [FromQuery] string? sort, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(ids)) return BadRequest(new ErrorResponse("invalid_query","Use ?ids=a,b,c"));
        var tokens = ids.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        try { return Ok(await _facade.CompareAsync(tokens, sort, ct)); }
        catch (ArgumentException ex) { return BadRequest(new ErrorResponse("invalid_query", ex.Message)); }
    }

    [HttpGet("catalog")]
    public async Task<IActionResult> Catalog(CancellationToken ct) => Ok(await _facade.CatalogAsync(ct));

    [HttpGet("items/{id}")]
    public async Task<IActionResult> GetById([FromRoute] string id, CancellationToken ct)
        => (await _facade.GetByIdAsync(id, ct)) is { } dto ? Ok(dto) : NotFound(new ErrorResponse("not_found",$"Item '{id}' não encontrado."));

    [HttpPut("items")]
    public async Task<IActionResult> Upsert([FromBody] ProductDto dto, CancellationToken ct)
    {
        if (dto.Price < 0) return BadRequest(new ErrorResponse("invalid_price","O preço precisa ser maior ou igual a Zero"));
        if (dto.Rating is <0 or >5) return BadRequest(new ErrorResponse("invalid_rating","A classificação deve ser [0,5]"));
        return Ok(await _facade.UpsertAsync(dto, ct));
    }

    [HttpDelete("items/{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id, CancellationToken ct)
        => await _facade.DeleteAsync(id, ct) ? NoContent() : NotFound(new ErrorResponse("not_found",$"Item '{id}' não encontrado."));
}