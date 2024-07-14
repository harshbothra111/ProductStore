using System.Security.Cryptography;
using System.Text.Json;
using System.Text;
using ProductStore.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using ProductStore.Domain.AggregateModels.ProductAggregate;

namespace ProductStore.Infrastructure.Data
{
    public class Seed
    {
        public static async Task SeedData(ProductDbContext context)
        {
            var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            if (!await context.Categories.AnyAsync())
            {
                var categoryData = await File.ReadAllTextAsync("Data/SeedData/CategorySeedData.json");
                var categories = JsonSerializer.Deserialize<List<Category>?>(categoryData, options);
                if (categories is not null)
                    await context.Categories.AddRangeAsync(categories);
            }

            if (!await context.SubCategories.AnyAsync())
            {
                var subCategoryData = await File.ReadAllTextAsync("Data/SeedData/SubCategorySeedData.json");
                var subCategories = JsonSerializer.Deserialize<List<SubCategory>?>(subCategoryData, options);
                if (subCategories is not null)
                    await context.SubCategories.AddRangeAsync(subCategories);
            }

            if (!await context.Products.AnyAsync())
            {
                var productData = await File.ReadAllTextAsync("Data/SeedData/ProductSeedData.json");
                var products = JsonSerializer.Deserialize<List<Product>?>(productData, options);
                if (products is not null)
                    await context.Products.AddRangeAsync(products);
            }

            await context.SaveChangesAsync();
        }
    }
}
