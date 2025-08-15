using ItemComparison.Api.Facade;
using ItemComparison.Application.Commands;
using ItemComparison.Application.Queries;
using ItemComparison.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure();
builder.Services.AddTransient<CompareItemsQuery>();
builder.Services.AddTransient<GetCatalogQuery>();
builder.Services.AddTransient<GetItemByIdQuery>();
builder.Services.AddTransient(services => new UpsertItemCommand(services.GetRequiredService<ItemComparison.Domain.Ports.IProductRepository>()));
builder.Services.AddTransient(services => new DeleteItemCommand(services.GetRequiredService<ItemComparison.Domain.Ports.IProductRepository>()));
builder.Services.AddTransient<ItemComparisonFacade>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

public partial class Program { }