using Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductWithBrandAndTypeSpecification:BaseSpecifications<Product,int>
    {
        //pass null to ctor mean there is no where
        public ProductWithBrandAndTypeSpecification():base(null)
        {
            AddInclude(P=>P.Brand);
            AddInclude(P=>P.Type);
        }
    }
}
