using DomainStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainStore.Interfaces
{
    public interface IUnitOfWork
    {
        public int SaveChanges();
        public IGenericRepository<TEntity,TKey> GetRepo<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
    }
}
