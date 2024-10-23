using DomainStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainStore.Specifications
{
    public class ProductSpecifications : Specifications<Product, int>
    {
        // Get All With Specifications
        public ProductSpecifications(string? Sort = null, int? TypeId = null, int? BrandId = null, int? PageIndex = null, int? PageSize = null, string? SearchByName = null)
        {
            if(Sort is not null)
            {
                switch(Sort)
                {
                    default:
                    case "name":
                        OrderBy = P => P.Name;
                        break;

                    case "PriceAsec":
                        OrderBy = P => P.Price;
                        break;

                    case "PriceDesc":
                        OrderByDesc = P => P.Price;
                        break;
                }
            }

            Criteria = P =>
                (TypeId == null || P.TypeId == TypeId) &&
                (BrandId == null || P.BrandId == BrandId) &&
                (string.IsNullOrEmpty(SearchByName) || P.Name.ToLower().Contains(SearchByName.ToLower()));

            if (PageIndex is not null && PageSize is not null)
            {
                this.PageIndex = PageIndex;
                this.PageSize = PageSize;
            }

            Includes.Add(P => P.Type);
            Includes.Add(P => P.Brand);
        }

        // Get One With Specifications
        public ProductSpecifications(int Id)
        {
            Criteria = P => P.Id == Id;
            Includes.Add(P => P.Type);
            Includes.Add(P => P.Brand);
        }
    }
}
