using DomainStore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RepositoryStore.Data.Contexts;
using RepositoryStore.Data.SeedData;
using RepositoryStore.Repositories;
using ServiceStore.Services;
using StackExchange.Redis;
using System.Net;
using WebAPIStore.Helpers;
using WebAPIStore.Middlewares;


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

            // Register The Connection To The Redis Server The The DI Container
            builder.Services.AddSingleton<IConnectionMultiplexer>(Provider =>
            {
                var Connection = builder.Configuration.GetConnectionString("Redis");

                // Like UseSqlServer
                return ConnectionMultiplexer.Connect(Connection);
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddScoped<IBasketRepository, BasketRepository>();

            builder.Services.AddScoped<IBasketService, BasketService>();

            // To Handle Validation Error With Configurations
            builder.Services.Configure<ApiBehaviorOptions>((Options) =>
            {
                Options.InvalidModelStateResponseFactory = (ContextActions) =>
                {
                    // Get The Entries Have A Error(s)
                    var Errors = ContextActions.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                             .SelectMany(P => P.Value.Errors)
                                             .Select(E => E.ErrorMessage)
                                             .ToList();

                    // Create A Response
                    var Response = new ApiValidationErrorResponse((int)HttpStatusCode.BadRequest, Errors);

                    // This Object Will Return In The Bad Request Response
                    return new BadRequestObjectResult(Response);
                };
            });

            builder.Services.AddControllers();

            // For Swagger
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

            app.UseMiddleware<ExceptionMiddleware>();

            // MiddleWare To Handle The Not Found EndPoint
            app.UseStatusCodePagesWithReExecute("/api/Errors");


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}
