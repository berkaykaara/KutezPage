using KutezApi.Model;
using KutezApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace KutezApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        // DI ile ProductService alınıyor
        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            var products = await _productService.GetAllWithCalculatedPriceAsync();
            return Ok(products);
        }
    }
}
