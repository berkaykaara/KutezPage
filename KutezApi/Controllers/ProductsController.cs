using KutezApi.Model;
using KutezApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KutezApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController()
        {
            _productService = new ProductService();
        }

        [HttpGet]
        public ActionResult<List<Product>> Get()
        {
            var products = _productService.GetAll();
            return Ok(products);
        }
    }
}
