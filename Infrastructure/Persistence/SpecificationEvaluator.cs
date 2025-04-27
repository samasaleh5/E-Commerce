using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class SpecificationEvaluator
    {
        //Create Query
        //Context.set<TEntity>();=>Input query param
        //Context.set<TEntity>().where(Criteria)  //if there is where cond

        public static IQueryable<TEntity> CreateQuery<TEntity,TKey>(IQueryable<TEntity> InputQuery,ISpecifications<TEntity,TKey> Spec) 
            where TEntity : ModelBase<TKey>
        {
            var Query = InputQuery;

            if (Spec.Criteria is not null)
                Query=Query.Where(Spec.Criteria);
            
            if(Spec.OrderBy is not null)
                Query=Query.OrderBy(Spec.OrderBy);

            if(Spec.OrderByDesc is not null)
                Query=Query.OrderByDescending(Spec.OrderByDesc);

            if(Spec.IncludeExpression is not null && Spec.IncludeExpression.Count > 0)
                Query = Spec.IncludeExpression.Aggregate(Query, (currentQuery, Exp) => currentQuery.Include(Exp));

            if(Spec.IsPaginated==true)
            {
                Query=Query.Skip(Spec.Skip).Take(Spec.Take);
            }

            return Query;
        }
    }
}
