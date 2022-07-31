using AutoMapper;
using Core.Entities;
using Core.Interfares;
using Core.Specifications;
using E_CommerceAPI.DTOs;
using E_CommerceAPI.Errors;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPI.Controllers
{

    public class ProductsController: BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTpyeRepo;

        public ProductsController(IGenericRepository<Product> productRepo, IMapper mapper, IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTpyeRepo)
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _productBrandRepo = productBrandRepo;
            _productTpyeRepo = productTpyeRepo;
        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();

            var products = await _productRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)] //this will help us to let the client know the endpoints available and what they would return kind of response they would get from the request
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

           var product = await _productRepo.GetEntityWithSpec(spec);

            if (product == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Product, ProductToReturnDto>(product);

           /* return new ProductToReturnDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                PictureUrl = product.PictureUrl,
                Price = product.Price,
                ProductBrand = product.ProductBrand.Name,
                ProductType = product.ProductType.Name
            };*/
        }

      //  [Cached(600)]
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandRepo.ListAllAsync());
        }

       // [Cached(600)]
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTpyeRepo.ListAllAsync());
        }


    }
}                                                                                                                                                       
