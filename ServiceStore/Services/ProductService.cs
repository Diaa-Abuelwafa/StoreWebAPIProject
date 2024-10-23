using DomainStore.DTOs.ProductDTOs;
using DomainStore.HelperClasses;
using DomainStore.Interfaces;
using DomainStore.Models;
using DomainStore.Specifications;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ServiceStore.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork Unit;
        private readonly IConfiguration Config;

        public ProductService(IUnitOfWork UnitOfWork, IConfiguration Config)
        {
            Unit = UnitOfWork;
            this.Config = Config;
        }

        public List<TypeBrandDTO> GetAllBrands()
        {
            Specifications<ProductBrand, int> Spec = new Specifications<ProductBrand, int>();

            List<ProductBrand> ProductBrands = Unit.GetRepo<ProductBrand, int>().GetAllWithSpec(Spec);

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

        public PaginationResponse<ProductDTO> GetAllProducts(string? Sort, int? TypeId, int? BrandId, int? PageIndex, int? PageSize, string? SearchByName)
        {
            ProductSpecifications Spec = new ProductSpecifications(Sort, TypeId, BrandId, PageIndex, PageSize, SearchByName);

            List<Product> Products = Unit.GetRepo<Product, int>().GetAllWithSpec(Spec);

            List<ProductDTO> ProductsDto = new List<ProductDTO>();

            foreach(var M in Products)
            {
                ProductDTO P = new ProductDTO();

                P.Id = M.Id;
                P.Name = M.Name;
                P.Description = M.Description;
                P.PictureUrl = $"{Config["BaseUrl"]}{M.PictureUrl}";
                P.Price = M.Price;
                P.TypeId = M.TypeId;
                P.TypeName = M.Type.Name;
                P.BrandId = M.BrandId;
                P.BrandName = M.Brand.Name;

                ProductsDto.Add(P);
            }

            // To Get Count After Filteration If Exsist
            ProductSpecifications SpecCriteria = new ProductSpecifications(TypeId:TypeId, BrandId:BrandId, SearchByName:SearchByName);
            int Count = Unit.GetRepo<Product, int>().GetCountAfterSpecifications(SpecCriteria);

            // Return The Pagination Response
            PaginationResponse<ProductDTO> ProductsResponse = new PaginationResponse<ProductDTO>(PageIndex, PageSize, Count, ProductsDto);

            return ProductsResponse;
        }

        public List<TypeBrandDTO> GetAllTypes()
        {
            Specifications<ProductType, int> Spec = new Specifications<ProductType, int>();

            List<ProductType> ProductTypes = Unit.GetRepo<ProductType, int>().GetAllWithSpec(Spec);

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

        public ProductDTO GetProductById(int Id)
        {

            ProductSpecifications Spec = new ProductSpecifications(Id);

            Product P = Unit.GetRepo<Product, int>().GetByIdWithSpec(Spec);

            if(P is null)
            {
                return null;
            }

            ProductDTO Product = new ProductDTO();

            Product.Id = P.Id;
            Product.Name = P.Name;
            Product.Description = P.Description;
            Product.PictureUrl = $"{Config["BaseUrl"]}{P.PictureUrl}";
            Product.Price = P.Price;
            Product.TypeId = P.TypeId;
            Product.TypeName = P.Type.Name;
            Product.BrandId = P.BrandId;
            Product.BrandName = P.Brand.Name;

            return Product;
        }
    }
}
