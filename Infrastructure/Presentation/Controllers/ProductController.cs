using Abstraction;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Dto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IServicesManger servicesManger):ControllerBase
    {
        //GetAllProduct
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts([FromQuery]ProductQueryParams productQuery)
        {
            var products = await servicesManger.productServices.GetAllProductsAsync(productQuery);

            //ok=> return json file of products
            return Ok(products);
        }

        //GetAllBrands
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var Brands = await servicesManger.productServices.GetAllBrandsAsync();

            //ok=> return json file of Brands
            return Ok(Brands);
        }


        //GetAllTypes
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
        {
            var Types = await servicesManger.productServices.GetAllTypesAsync();

            //ok=> return json file of Types
            return Ok(Types);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var Product=await servicesManger.productServices.GetProductByIdAsync(id);
            return Ok(Product);
        }

    }
}
