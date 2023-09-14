using Microsoft.AspNetCore.Mvc;

namespace ApiFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public string GetProducts()
        {
            return "this will be a list of products";
        }

        [HttpGet("{id}")]
        public string GetProducts(int id)
        {
            return "this will be a single product";
        }
    }
}
