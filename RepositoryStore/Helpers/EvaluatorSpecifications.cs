﻿using DomainStore.Interfaces;
using DomainStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryStore.Helpers
{
    public static class EvaluatorSpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> BaseQuery, ISpecifications<TEntity, TKey> Spec)
        {
            var Query = BaseQuery;

            if (Spec.Criteria is not null)
            {
                Query = Query.Where(Spec.Criteria);
            }

            foreach (var I in Spec.Includes)
            {
                Query = Query.Include(I);
            }

            if (Spec.PageIndex is not null && Spec.PageSize is not null)
            {
                Query = Query.Skip(((int)Spec.PageIndex - 1) * (int)Spec.PageSize);
                Query = Query.Take((int)Spec.PageSize);
            }

            if (Spec.OrderBy is not null)
            {
                Query = Query.OrderBy(Spec.OrderBy);
            }

            if (Spec.OrderByDesc is not null)
            {
                Query = Query.OrderByDescending(Spec.OrderByDesc);
            }

            return Query;
        }
    }
}
