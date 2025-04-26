using Domain.Contracts;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey>
        where TEntity : ModelBase<TKey>
    {
        public BaseSpecifications(Expression<Func<TEntity, bool>>? PassedExpression)
        {
            //to make private set for criteria
            Criteria = PassedExpression;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        public List<Expression<Func<TEntity, object>>> IncludeExpression { get; } = new List<Expression<Func<TEntity, object>>>();
        
        //function to add include
        protected void AddInclude(Expression<Func<TEntity, object>> IncludeExp)
        {
            IncludeExpression.Add(IncludeExp);
        }
    }
}
