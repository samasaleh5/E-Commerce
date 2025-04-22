using Domain.Contracts;
using Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Data
{
    public class DbInitializer(StoreDBContext context) : IDbInitializer
    {
        public async Task InitializerAsync()
        {
            //to apply any new migration and update database

            if((await context.Database.GetPendingMigrationsAsync()).Any())
            {
                await context.Database.MigrateAsync();
            }

            //check if there is a initialize data or not if there is data not add if no data found add  
            try
            {
                if (!context.Set<ProductBrand>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeds\brands.json");
                    var Objects = JsonSerializer.Deserialize<List<ProductBrand>>(data);

                    if (Objects is not null && Objects.Any())
                    {
                        context.Set<ProductBrand>().AddRange(Objects);
                        await context.SaveChangesAsync();
                    }
                }

                if (!context.Set<ProductType>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeds\types.json");
                    var Objects = JsonSerializer.Deserialize<List<ProductType>>(data);

                    if (Objects is not null && Objects.Any())
                    {
                        context.Set<ProductType>().AddRange(Objects);
                        await context.SaveChangesAsync();
                    }
                }

                if (!context.Set<Product>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeds\products.json");
                    var Objects = JsonSerializer.Deserialize<List<Product>>(data);

                    if (Objects is not null && Objects.Any())
                    {
                        context.Set<Product>().AddRange(Objects);
                        await context.SaveChangesAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
