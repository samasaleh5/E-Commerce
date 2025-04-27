using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQueryParams
    {
        private const int DefaultPageSize = 5;
        private const int MaximumPageSize = 10;
        public int? BrandId {  get; set; }

        public int? TypeId { get; set; }

        public ProductSortingOptions SortingOption { get; set; }

        public string? SearchValue { get; set; }

        public int PageIndex { get; set; } = 1;

       private int PageSize= DefaultPageSize;

        public int Pagesize
        {
            get { return PageSize; }
            set { PageSize = value > MaximumPageSize ? MaximumPageSize : value; }
        }


    }
}
