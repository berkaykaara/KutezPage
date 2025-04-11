using KutezApi.Model;
using System.Text.Json;

namespace KutezApi.Services
{
    public class ProductService
    {
        private readonly string _jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/data/products.json");

        public List<Product> GetAll()
        {
            if (!File.Exists(_jsonPath)) return new List<Product>();

            var json = File.ReadAllText(_jsonPath);
            var products = JsonSerializer.Deserialize<List<Product>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return products ?? new List<Product>();
        }
    }
}