using Domain.Contracts;
using Domain.Models;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDBContext context) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _Repositories= new Dictionary<string, object>();
        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : ModelBase<Tkey>
        {
            //TypeName
            var TypeName=typeof(TEntity).Name;

            //check if typename found in repo before of not

            //if found return type
            if(_Repositories.ContainsKey(TypeName))
                return (IGenericRepository<TEntity,Tkey>) _Repositories[TypeName];

            //if not make new repo 
            var Repo =new GenericRepository<TEntity,Tkey>(context);
            _Repositories.Add(TypeName, Repo);
            return Repo;
  
        }

        public async Task<int> SaveChangeAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
