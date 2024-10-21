﻿using DomainStore.DTOs.ProductDTOs;
using DomainStore.Interfaces;
using DomainStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStore.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork Unit;

        public ProductService(IUnitOfWork UnitOfWork)
        {
            Unit = UnitOfWork;
        }

        public List<TypeBrandDTO> GetAllBrands()
        {
            List<ProductBrand> ProductBrands = Unit.GetRepo<ProductBrand, int>().GetAll();

            List<TypeBrandDTO> Brands = new List<TypeBrandDTO>();

            foreach(var Item in ProductBrands)
            {
                TypeBrandDTO M = new TypeBrandDTO();

                M.Name = Item.Name;
                M.CreatedAt = Item.CreatedAt;

                Brands.Add(M);
            }

            return Brands;
        }

        public List<ProductDTO> GetAllProducts()
        {
            List<Product> Products = Unit.GetRepo<Product, int>().GetAll();

            List<ProductDTO> ProductsDto = new List<ProductDTO>();

            foreach(var M in Products)
            {
                ProductDTO P = new ProductDTO();

                P.Id = M.Id;
                P.Name = M.Name;
                P.Description = M.Description;
                P.PictureUrl = M.PictureUrl;
                P.Price = M.Price;
                P.TypeId = M.TypeId;
                P.TypeName = M.Type.Name;
                P.BrandId = M.BrandId;
                P.BrandName = M.Brand.Name;

                ProductsDto.Add(P);
            }

            return ProductsDto;
        }

        public List<TypeBrandDTO> GetAllTypes()
        {
            List<ProductType> ProductTypes = Unit.GetRepo<ProductType, int>().GetAll();

            List<TypeBrandDTO> Types = new List<TypeBrandDTO>();

            foreach (var Item in ProductTypes)
            {
                TypeBrandDTO M = new TypeBrandDTO();

                M.Name = Item.Name;
                M.CreatedAt = Item.CreatedAt;

                Types.Add(M);
            }

            return Types;
        }

        public ProductDTO GetProductById(int? Id)
        {
            Product P = Unit.GetRepo<Product, int>().GetById(Id);

            if(P is null)
            {
                return null;
            }

            ProductDTO Product = new ProductDTO();

            Product.Id = P.Id;
            Product.Name = P.Name;
            Product.Description = P.Description;
            Product.PictureUrl = P.PictureUrl;
            Product.Price = P.Price;
            Product.TypeId = P.TypeId;
            Product.TypeName = P.Type.Name;
            Product.BrandId = P.BrandId;
            Product.BrandName = P.Brand.Name;

            return Product;
        }
    }
}