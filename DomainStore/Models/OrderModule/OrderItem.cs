using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainStore.Models.OrderModule
{
    public class OrderItem : BaseEntity<int>
    {
        public ProductItemOrdered product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
    }
}
