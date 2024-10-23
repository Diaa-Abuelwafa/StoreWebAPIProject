using DomainStore.DTOs.ProductDTOs;
using DomainStore.HelperClasses;
using DomainStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainStore.Interfaces
{
    public interface IProductService
    {
        public PaginationResponse<ProductDTO> GetAllProducts(string? Sort, int? TypeId, int? BrandId, int? PageIndex, int? PageSize, string? SearchByName);
        public ProductDTO GetProductById(int Id);
        public List<TypeBrandDTO> GetAllBrands();
        public List<TypeBrandDTO> GetAllTypes();
    }
}
