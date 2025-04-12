using KutezApi.Model;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace KutezApi.Services
{
    public class GoldPriceService
    {
        private readonly HttpClient _httpClient;
        private readonly GoldApiSettings _settings;

        public GoldPriceService(IOptions<GoldApiSettings> settings)
        {
            _settings = settings.Value;
            _httpClient = new HttpClient();

            _httpClient.DefaultRequestHeaders.Add("x-access-token", _settings.ApiKey);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // Asenkron olarak gram başına 24 ayar altın fiyatını döndürür
        public async Task<double> GetGoldPricePerGramAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_settings.Endpoint);

                if (!response.IsSuccessStatusCode)
                {
                    return 60.0; // fallback fiyat (USD/gram)
                }

                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);

                return doc.RootElement.GetProperty("price_gram_24k").GetDouble();
            }
            catch
            {
                return 60.0; // hata durumunda fallback
            }
        }
    }
}
