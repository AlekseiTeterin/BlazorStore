using MyStore.Models;
using Microsoft.AspNetCore.Mvc;
using MyStore.WebApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var dbPath = "myapp.db";
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(
   options => options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddCors();

builder.Services.AddScoped<IProductsRepository, ProductRepository>();
builder.Services.AddControllers();
var app = builder.Build();

// CORS
app.UseCors(policy => policy
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin =>
        origin is "https://localhost:7074"
            or "http://localhost:5259")
    .AllowCredentials()
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// "/add_product" - RPC
// "/catalog/products" - REST
app.MapPost("/add_product", async ([FromBody] Product product, [FromServices] IProductsRepository productRepository) =>
{
    await productRepository.AddProduct(product);
}).WithName("Добавляет товар");

app.MapGet("/get_products", async ([FromServices] IProductsRepository productRepository) =>
{
    var products = await productRepository.GetProducts();
    if (products == null)
    {
        return Results.NotFound(new { message = "Товар не найден" });
    }
    return Results.Ok(products);
}).WithName("Получает товар");

app.MapGet("/get_product", async ([FromServices] IProductsRepository productRepository,
    [FromQuery] int productId) =>
{
    var product = await productRepository.GetProductById(productId);
    if (product == null)
    {
        return Results.NotFound(new { message = "Товар не найден" });
    }
    return Results.Ok(product);
});

app.MapPost("/update_product",
    async ([FromServices] IProductsRepository productRepository,
        [FromQuery] int productId, [FromBody] Product newProduct) =>
    {
        await productRepository.UpdateProduct(newProduct, productId);
    });

app.MapPost("/delete_product",
    async ([FromServices] IProductsRepository productRepository,
    [FromQuery] int productId) =>
    {
        await productRepository.DeleteProduct(productId);
    });

app.Run();
