using Domain.Models.Products;
using Shared;
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
        public ProductWithBrandAndTypeSpecification(int? brandId, int? typeId, ProductSortingOptions sortingOption)
            : base(p => (!brandId.HasValue || p.BrandId == brandId)
                     && (!typeId.HasValue || p.TypeId == typeId))
        {
            AddInclude(P => P.Brand);
            AddInclude(P => P.Type);

            switch (sortingOption)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(P => P.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDesc(P => P.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(P => P.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDesc(P => P.Price);
                    break;
                default:
                    break;
            }
        }

        public ProductWithBrandAndTypeSpecification(int id):base(p=>p.Id==id)
        {
            AddInclude(P => P.Brand);
            AddInclude(P => P.Type);
        }
    }
}
