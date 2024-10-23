using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainStore.HelperClasses
{
    public class ProductSpecParams
    {
        public string? Sort { get; set; }
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public string? SearchByName { get; set; }
    }
}
