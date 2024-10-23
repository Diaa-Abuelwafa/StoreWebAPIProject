using DomainStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainStore.HelperClasses
{
    public class PaginationResponse<TEntity> where TEntity : class
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public int Count { get; set; }
        public List<TEntity> Data { get; set; } = new List<TEntity>();

        public PaginationResponse(int? PI, int? PS, int C, List<TEntity> D)
        {
            PageIndex = PI;
            PageSize = PS;
            Count = C;
            Data = D;
        }
    }
}
