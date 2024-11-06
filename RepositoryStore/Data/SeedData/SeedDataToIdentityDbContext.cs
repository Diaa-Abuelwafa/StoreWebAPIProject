using DomainStore.Identity;
using RepositoryStore.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RepositoryStore.Data.SeedData
{
    public static class SeedDataToIdentityDbContext
    {
        public static void Seed(IdentityStoreDbContext Context)
        {
            if (Context.Addresses.Count() == 0)
            {
                // Seed Data Of Brands
                var AddressesJson = File.ReadAllText("../RepositoryStore/Data/SeedData/IdentitySeedData/Addresses.json");

                List<Address>? Addresses = JsonSerializer.Deserialize<List<Address>>(AddressesJson);

                if (Addresses is not null && Addresses.Count() > 0)
                {
                    Context.Addresses.AddRange(Addresses);
                    Context.SaveChanges();
                }
            }
        }
    }
}
