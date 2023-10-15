using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Specifications;
using ApiFinal.Dtos;
using AutoMapper;
using ApiFinal.Errors;
using ApiFinal.Helpers;

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
        public async Task<ActionResult<IReadOnlyList<Pagination<ProductsToReturnDto>>>> GetProducts([FromQuery] ProductsSpecParams productParams)
        {
            try 
            {
                var spec = new ProductsWithTypesandBrandSpecification(productParams);

                var product = await _productsRepo.ListAsync(spec);

                var countSpec = new ProductsWithFilterForCountSpecification(productParams);
                
                var totalItems = await _productsRepo.CountAsync(countSpec);

                var products = await _productsRepo.ListAsync(spec);

                var data = _mapper.Map<IReadOnlyList<Product> ,IReadOnlyList<ProductsToReturnDto>>(products);

                return Ok(new Pagination<ProductsToReturnDto>(productParams.PageIndex,productParams.PageSize,totalItems,data));

            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductsToReturnDto>> GetProducts(int id)
        {
            var spec = new ProductsWithTypesandBrandSpecification(id);

            var product = await _productsRepo.GetEntityWithSpec(spec);

            if(product == null) return NotFound(new ApiResponse(404));  

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
