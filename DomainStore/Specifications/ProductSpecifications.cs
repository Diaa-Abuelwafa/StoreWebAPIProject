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
        // GetAll
        public ProductSpecifications()
        {
            Includes.Add(P => P.Type);
            Includes.Add(P => P.Brand);
        }

        // GetById
        public ProductSpecifications(int Id)
        {
            Criteria = P => P.Id == Id;
            Includes.Add(P => P.Type);
            Includes.Add(P => P.Brand);
        }
    }
}
