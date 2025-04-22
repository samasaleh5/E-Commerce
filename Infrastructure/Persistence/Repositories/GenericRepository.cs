using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, Tkey>(StoreDBContext context) : IGenericRepository<TEntity, Tkey>
        where TEntity:ModelBase<Tkey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        =>await context.Set<TEntity>().ToListAsync();

        public async Task<TEntity> GetByIdAsync(Tkey id)
        => await context.Set<TEntity>().FindAsync(id);

        public void Add(TEntity entity)
        =>context.Set<TEntity>().Add(entity);
      
        public void Update(TEntity entity)
        =>context.Set<TEntity>().Update(entity);
        public void Delete(TEntity entity)
        => context.Set<TEntity>().Remove(entity);
    }
}
