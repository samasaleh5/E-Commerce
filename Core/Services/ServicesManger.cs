using Abstraction;
using AutoMapper;
using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServicesManger(IUnitOfWork unitOfWork,IMapper mapper) : IServicesManger
    {
        //lazy object to be lazy until i recall it
        private readonly Lazy<IProductServices> _LazyProductServices = new Lazy<IProductServices>(() => new ProductServices(unitOfWork,mapper)); 

        public IProductServices productServices => _LazyProductServices.Value;
    }
}
