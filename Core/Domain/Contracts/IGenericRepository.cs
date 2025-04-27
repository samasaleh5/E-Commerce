using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity, Tkey> where TEntity : ModelBase<Tkey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(Tkey id);

        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> Spec);

        Task<TEntity> GetByIdAsync(ISpecifications<TEntity, Tkey> Spec);

        Task <int> CountAsync(ISpecifications<TEntity,Tkey> Spec);
        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}
