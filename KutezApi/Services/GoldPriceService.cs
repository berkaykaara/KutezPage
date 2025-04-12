using System.Net.Http.Headers;
using System.Text.Json;

namespace KutezApi.Services
{
    public class GoldPriceService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "goldapi-17yeanmsm9deuf7l-io"; // ← Buraya kendi GoldAPI key'ini gir
        private const string Url = "https://www.goldapi.io/api/XAU/USD";

        public GoldPriceService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("x-access-token", ApiKey);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<double> GetGoldPricePerGramAsync()
        {
            var response = await _httpClient.GetAsync(Url);
            if (!response.IsSuccessStatusCode)
            {
                // Gerekirse logla veya fallback fiyat döndür
                return 60.0; // fallback fiyat (USD/gram)
            }

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            // gram_price key'ini direkt çekiyoruz
            double pricePerGram = doc.RootElement.GetProperty("price_gram_24k").GetDouble();

            return pricePerGram;
        }
    }
}
