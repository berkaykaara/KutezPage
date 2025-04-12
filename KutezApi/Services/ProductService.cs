using KutezApi.Model;
using System.Text.Json;

namespace KutezApi.Services
{
    public class ProductService
    {
        private readonly string _jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/data/products.json");

        public async Task<List<Product>> GetAllWithCalculatedPriceAsync()
        {
            if (!File.Exists(_jsonPath)) return new List<Product>();

            var json = File.ReadAllText(_jsonPath);
            var products = JsonSerializer.Deserialize<List<Product>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<Product>();

            var goldPriceService = new GoldPriceService();
            double goldPricePerGram = await goldPriceService.GetGoldPricePerGramAsync();

            foreach (var product in products)
            {
                product.Price = (product.PopularityScore + 1) * product.Weight * goldPricePerGram;
            }

            return products;
        }
    }
}