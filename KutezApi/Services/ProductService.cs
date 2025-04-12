using KutezApi.Model;
using System.Text.Json;

namespace KutezApi.Services
{
    public class ProductService
    {
        private readonly GoldPriceService _goldPriceService;
        private readonly string _jsonPath;

        public ProductService(GoldPriceService goldPriceService)
        {
            _goldPriceService = goldPriceService;
            _jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/data/products.json");
        }

        public async Task<List<Product>> GetAllWithCalculatedPriceAsync()
        {
            if (!File.Exists(_jsonPath)) return new List<Product>();

            var json = await File.ReadAllTextAsync(_jsonPath);

            var products = JsonSerializer.Deserialize<List<Product>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<Product>();

            double goldPricePerGram = await _goldPriceService.GetGoldPricePerGramAsync();

            foreach (var product in products)
            {
                product.Price = (product.PopularityScore + 1) * product.Weight * goldPricePerGram;
            }

            return products;
        }
    }
}
