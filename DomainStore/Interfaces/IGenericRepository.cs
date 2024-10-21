using DomainStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainStore.Interfaces
{
    public interface IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        public List<TEntity> GetAll();
        public TEntity GetById(int? Id);
        public void Add(TEntity Item);
        public void Update(TEntity Item);
        public void Delete(TEntity Item);
    }
}
