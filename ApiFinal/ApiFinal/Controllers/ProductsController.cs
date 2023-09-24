﻿using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Specifications;
using ApiFinal.Dtos;
using AutoMapper;
using ApiFinal.Errors;

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
        public async Task<ActionResult<IReadOnlyList<ProductsToReturnDto>>> GetProducts(int? brandId, int? typeId ,string? sort)
        {
           var spec = new  ProductsWithTypesSpecification(sort,brandId,typeId);
          
           var product = await _productsRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<ProductsToReturnDto>>(product));
         
            
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductsToReturnDto>> GetProducts(int id)
        {
            var spec = new ProductsWithTypesSpecification(id);

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
