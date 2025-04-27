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
        public ProductWithBrandAndTypeSpecification(ProductQueryParams productQuery)
            : base(p => (!productQuery.BrandId.HasValue || p.BrandId == productQuery.BrandId)
                     && (!productQuery.TypeId.HasValue || p.TypeId == productQuery.TypeId)
                     && (string.IsNullOrEmpty(productQuery.SearchValue)||p.Name.ToLower().Contains(productQuery.SearchValue.ToLower())))

        {
            AddInclude(P => P.Brand);
            AddInclude(P => P.Type);

            switch (productQuery.SortingOption)
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
