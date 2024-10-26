using DomainStore.Models.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainStore.DTOs.BasketDTOs
{
    public class BasketDTO
    {
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }
    }
}
