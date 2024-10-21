using DomainStore.Interfaces;
using DomainStore.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryStore.Data.Contexts;
using RepositoryStore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryStore.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly AppDbContext Context;
        public GenericRepository(AppDbContext Context)
        {
            this.Context = Context;
        }
        public List<TEntity> GetAll()
        {
            if(typeof(TEntity) == typeof(Product))
            {
                return Context.Products.Include(x => x.Type).Include(x => x.Brand).ToList() as List<TEntity>;
            }

            return Context.Set<TEntity>().ToList();
        }

        public TEntity GetById(TKey Id)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return Context.Products.Include(x => x.Type).Include(x => x.Brand).FirstOrDefault(x => x.Id == Id as int?) as TEntity;
            }

            return Context.Set<TEntity>().Find(Id);
        }
        public void Add(TEntity Item)
        {
            Context.Add(Item);
        }
        public void Update(TEntity Item)
        {
            Context.Update(Item);
        }

        public void Delete(TEntity Item)
        {
            Context.Remove(Item);
        }

        public List<TEntity> GetAllWithSpec(ISpecifications<TEntity, TKey> Spec)
        {
            return EvaluatorSpecifications<TEntity, TKey>.GetQuery(Context.Set<TEntity>(), Spec).ToList();
        }

        public TEntity GetByIdWithSpec(ISpecifications<TEntity, TKey> Spec)
        {
            return EvaluatorSpecifications<TEntity, TKey>.GetQuery(Context.Set<TEntity>(), Spec).FirstOrDefault();
        }
    }
}
