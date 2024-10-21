using DomainStore.Models;
using RepositoryStore.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RepositoryStore.Data.SeedData
{
    public static class SeedDataToAppDbContext
    {
        public static void Seed(AppDbContext Context)
        {
            if(Context.Brands.Count() == 0)
            {
                // Seed Data Of Brands
                var BrandsJson = File.ReadAllText("../RepositoryStore/Data/SeedData/ProductSeedData/brands.json");

                List<ProductBrand>? Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsJson);

                if (Brands is not null && Brands.Count() > 0)
                {
                    Context.Brands.AddRange(Brands);
                    Context.SaveChanges();
                }
            }

            if(Context.Types.Count() == 0)
            {
                // Seed Data Of Types
                var TypesJson = File.ReadAllText("../RepositoryStore/Data/SeedData/ProductSeedData/types.json");

                List<ProductType>? Types = JsonSerializer.Deserialize<List<ProductType>>(TypesJson);

                if (Types is not null && Types.Count() > 0)
                {
                    Context.Types.AddRange(Types);
                    Context.SaveChanges();
                }
            }

            if (Context.Products.Count() == 0)
            {
                // Seed Data Of Products
                var ProductsJson = File.ReadAllText("../RepositoryStore/Data/SeedData/ProductSeedData/products.json");

                List<Product>? Products = JsonSerializer.Deserialize<List<Product>>(ProductsJson);

                if (Products is not null && Products.Count() > 0)
                {
                    Context.Products.AddRange(Products);
                    Context.SaveChanges();
                }
            }
        }
    }
}
