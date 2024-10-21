using DomainStore.Interfaces;
using Microsoft.EntityFrameworkCore;
using RepositoryStore.Data.Contexts;
using RepositoryStore.Data.SeedData;
using RepositoryStore.Repositories;
using ServiceStore.Services;

namespace WebAPIStore
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("Cs"));
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Make Update-Database Command To Apply Migration(s) Not Applied
            using(var Scope = app.Services.CreateScope())
            {
                var Context = Scope.ServiceProvider.GetRequiredService<AppDbContext>();

                try
                {
                    // Update-Database 
                    await Context.Database.MigrateAsync();

                    // Add Seed Data To DB For First Running
                    SeedDataToAppDbContext.Seed(Context);
                }
                catch(Exception Ex)
                {
                    Console.WriteLine(Ex.Message);
                }
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}
