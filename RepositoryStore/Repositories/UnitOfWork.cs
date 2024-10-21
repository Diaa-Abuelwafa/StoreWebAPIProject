using DomainStore.Interfaces;
using DomainStore.Models;
using RepositoryStore.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryStore.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext Context;
        private Dictionary<string, object> Repositories;
        public UnitOfWork(AppDbContext Context)
        {
            this.Context = Context;
        }

        public IGenericRepository<TEntity,TKey> GetRepo<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var Type = typeof(TEntity).Name;

            Repositories = new Dictionary<string, object>();

            if (!Repositories.ContainsKey(Type))
            {
                IGenericRepository<TEntity, TKey> Repo = new GenericRepository<TEntity, TKey>(Context);

                Repositories.Add(Type, Repo);
            }

            return (IGenericRepository<TEntity,TKey>) Repositories[Type];
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }
    }
}
