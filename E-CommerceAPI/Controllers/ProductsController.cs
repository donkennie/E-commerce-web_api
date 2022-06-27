using Core.Entities;
using Core.Interfares;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class ProductsController: ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }


        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _repo.GetProductsAsync();

            return Ok(products);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _repo.GetProductByIdAsync(id);
        }


    }
}
