using DomainStore.DTOs.ProductDTOs;
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
        public List<ProductDTO> GetAllProducts();
        public ProductDTO GetProductById(int Id);
        public List<TypeBrandDTO> GetAllBrands();
        public List<TypeBrandDTO> GetAllTypes();
    }
}
