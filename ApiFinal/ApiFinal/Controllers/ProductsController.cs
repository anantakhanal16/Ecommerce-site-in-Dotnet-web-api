using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Specifications;
using ApiFinal.Dtos;
using AutoMapper;

namespace ApiFinal.Controllers
{
   
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;

        private readonly IMapper _mapper;
        public ProductsController(
          IGenericRepository<Product> productsRepo,
          IGenericRepository<ProductBrand> productBrandRepo,
          IGenericRepository<ProductType> productTypeRepo,
          IMapper mapper
          ) 
        {
            _productsRepo = productsRepo;
            _productBrandRepo= productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductsToReturnDto>>> GetProducts()
        {
           var spec = new  ProductsWithTypesSpecification();
          
           var product = await _productsRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<ProductsToReturnDto>>(product));
         
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductsToReturnDto>> GetProducts(int id)
        {
            var spec = new ProductsWithTypesSpecification(id);

            var product = await _productsRepo.GetEntityWithSpec(spec);

            return _mapper.Map<ProductsToReturnDto>(product);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<ProductBrand>> GetProductsBrands()
        {
            return Ok( await _productBrandRepo.ListAllAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<ProductType>> GetProductsTypes()
        {
            return Ok(await _productTypeRepo.ListAllAsync());
        }
    }
}
