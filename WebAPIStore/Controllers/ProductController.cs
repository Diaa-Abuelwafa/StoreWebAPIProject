using DomainStore.DTOs.ProductDTOs;
using DomainStore.HelperClasses;
using DomainStore.Interfaces;
using DomainStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPIStore.Attributes;
using WebAPIStore.Helpers;

namespace WebAPIStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService ProductService;
        public ProductController(IProductService ProductService)
        {
            this.ProductService = ProductService;
        }

        [Cached(100)]
        [HttpGet]
        [ProducesResponseType(typeof(List<PaginationResponse<ProductDTO>>), (int)HttpStatusCode.OK)]
        public IActionResult GetAllProducts([FromQuery] ProductSpecParams Spec)
        {
            PaginationResponse<ProductDTO> Products = ProductService.GetAllProducts(Spec.Sort, Spec.TypeId, Spec.BrandId, Spec.PageIndex, Spec.PageSize, Spec.SearchByName);

            return Ok(Products);
        }

        [HttpGet]
        [Route("Brands")]
        [ProducesResponseType(typeof(List<TypeBrandDTO>), (int)HttpStatusCode.OK)]
        [Authorize]
        public IActionResult GetAllBrands()
        {
            List<TypeBrandDTO> Brands = ProductService.GetAllBrands();

            return Ok(Brands);
        }

        [HttpGet("Types")]
        [ProducesResponseType(typeof(List<TypeBrandDTO>), (int)HttpStatusCode.OK)]
        public IActionResult GetAllTypes()
        {
            List<TypeBrandDTO> Types = ProductService.GetAllTypes();

            return Ok(Types);
        }

        [HttpGet("{Id:int}")]
        // More Than One Expected Return 
        [ProducesResponseType(typeof(List<TypeBrandDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.NotFound)]
        public IActionResult GetProductById(int Id)
        {
            ProductDTO Product = ProductService.GetProductById(Id);

            if (Product is not null)
            {
                return Ok(Product);
            }

            //return NotFound(new ApiErrorResponse(404, "This Resource Not Found"));
            return NotFound(new ApiErrorResponse((int)HttpStatusCode.NotFound));
        }
    }
}
