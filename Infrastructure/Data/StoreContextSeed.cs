using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {

        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {   
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                if (!context.ProductBrands.Any())
                {
                    using var transaction = context.Database.BeginTransaction();
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/Brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (var item in brands)
                    {
                        context.ProductBrands.Add(item);
                    }

                 ///   context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT ProductBrands ON");

                    await context.SaveChangesAsync();

                //    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT ProductBrands OFF");

                   transaction.Commit();
                }

                if (!context.ProductTypes.Any())
                {
                    using var transaction = context.Database.BeginTransaction();
                    var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/Types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var item in types)
                    {
                        context.ProductTypes.Add(item);
                    }

                //    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT ProductTypes ON");

                    await context.SaveChangesAsync();

                 //   context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT ProductTypes OFF");

                    transaction.Commit();
                }

                if (!context.Products.Any())
                {
                    using var transaction = context.Database.BeginTransaction();
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/Products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }

                   // context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Products ON");

                    await context.SaveChangesAsync();

                   // context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Products OFF");

                    transaction.Commit();
                }

             /*   if (!context.DeliveryMethods.Any())
                {
                    var dmData = File.ReadAllText(path + @"/Data/SeedData/delivery.json");
                    var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(dmData);

                    foreach (var item in methods)
                    {
                        context.DeliveryMethods.Add(item);
                    }

                    await context.SaveChangesAsync();
                }*/
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
