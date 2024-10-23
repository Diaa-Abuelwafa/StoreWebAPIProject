using DomainStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainStore.Interfaces
{
    public interface ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity, bool>> Criteria { get; set; }
        public List<Expression<Func<TEntity, object>>> Includes { get; set; }
        public Expression<Func<TEntity, object>> OrderBy { get; set; }
        public Expression<Func<TEntity, object>> OrderByDesc { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
    }
}
