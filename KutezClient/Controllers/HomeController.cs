using KutezClient.Services;
using Microsoft.AspNetCore.Mvc;

namespace KutezClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductService _productService;

        // ProductService dependency injection ile al�n�r
        public HomeController(ProductService productService)
        {
            _productService = productService;
        }

        // Index aksiyonu: filtreleme ve s�ralama yap�lm�� �r�n listesini d�nd�r�r
        // Parametreler opsiyonel (nullable) olarak al�n�r
        public async Task<IActionResult> Index(double? minPrice, double? maxPrice, double? minRating, string? sortBy)
        {
            // API'den �r�n verileri �ekilir
            var products = await _productService.GetAllAsync();

            // Fiyat filtresi (minimum)
            if (minPrice.HasValue)
                products = products.Where(p => p.Price >= minPrice.Value).ToList();

            // Fiyat filtresi (maksimum)
            if (maxPrice.HasValue)
                products = products.Where(p => p.Price <= maxPrice.Value).ToList();

            // Puan filtresi (pop�lerlik skoru * 5 = y�ld�zl� sistem gibi d���n�lm��)
            if (minRating.HasValue)
                products = products.Where(p => p.PopularityScore * 5 >= minRating.Value).ToList();

            // S�ralama (sortBy parametresine g�re)
            products = sortBy switch
            {
                "priceAsc" => products.OrderBy(p => p.Price).ToList(),               // Fiyata g�re artan
                "priceDesc" => products.OrderByDescending(p => p.Price).ToList(),    // Fiyata g�re azalan
                "ratingDesc" => products.OrderByDescending(p => p.PopularityScore).ToList(), // Puana g�re azalan
                "ratingAsc" => products.OrderBy(p => p.PopularityScore).ToList(),    // Puana g�re artan
                _ => products // s�ralama yap�lmaz
            };

            // Sonu� View'e g�nderilir
            return View(products);
        }
    }
}
