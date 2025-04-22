using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction
{
    public interface IServicesManger
    {
        //any service put here and implement in servicesmanger
        public IProductServices productServices { get; }

    }
}
