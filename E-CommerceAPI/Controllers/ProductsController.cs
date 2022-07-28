using AutoMapper;
using Core.Entities;
using Core.Interfares;
using Core.Specifications;
using E_CommerceAPI.DTOs;
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
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

           var product = await _productRepo.GetEntityWithSpec(spec);

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
