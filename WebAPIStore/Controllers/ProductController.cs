﻿using DomainStore.DTOs.ProductDTOs;
using DomainStore.Interfaces;
using DomainStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            List<ProductDTO> Products = ProductService.GetAllProducts();

            return Ok(Products);
        }

        [HttpGet]
        [Route("Brands")]
        public IActionResult GetAllBrands()
        {
            List<TypeBrandDTO> Brands = ProductService.GetAllBrands();

            return Ok(Brands);
        }

        [HttpGet("Types")]
        public IActionResult GetAllTypes()
        {
            List<TypeBrandDTO> Types = ProductService.GetAllTypes();

            return Ok(Types);
        }

        [HttpGet("{Id:int}")]
        public IActionResult GetProductById(int? Id)
        {
            if(Id is null)
            {
                return BadRequest();
            }

            ProductDTO Product = ProductService.GetProductById(Id);

            if(Product is not null)
            {
                return Ok(Product);
            }

            return NotFound();
        }
    }
}
