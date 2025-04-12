using KutezApi.Model;
using KutezApi.Services;

var builder = WebApplication.CreateBuilder(args);

// GoldApi yap�land�rmas�n� ba�la
builder.Services.Configure<GoldApiSettings>(builder.Configuration.GetSection("GoldApi"));

builder.Services.AddControllers();

// Servisleri DI container'a ekle
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<GoldPriceService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Geli�tirme ortam�nda Swagger UI'� etkinle�tir
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers();
app.Run();
