 using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ISpecifications<TEntity,TKey> where TEntity : ModelBase<TKey>
    {
       Expression<Func<TEntity,bool>>? Criteria { get; }
       List<Expression<Func<TEntity,object>>> IncludeExpression { get; }
       
       Expression<Func<TEntity,object>>OrderBy { get; }
       Expression<Func<TEntity,object>>OrderByDesc { get; }

       public int Take { get; }
       public int Skip { get; }

       //For Frontend want to apply pagination or not
       public bool IsPaginated { get; set; }

    }
}
