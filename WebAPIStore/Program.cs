using DomainStore.Identity;
using DomainStore.Interfaces;
using DomainStore.Interfaces.Account;
using DomainStore.Interfaces.Caching;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RepositoryStore.Data.Contexts;
using RepositoryStore.Data.SeedData;
using RepositoryStore.Repositories;
using ServiceStore.Services;
using ServiceStore.Services.Account;
using ServiceStore.Services.Chacing;
using StackExchange.Redis;
using System.Net;
using System.Text;
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

            builder.Services.AddDbContext<IdentityStoreDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("Identity"));
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<IdentityStoreDbContext>();

            // Register The Connection To The Redis Server The The DI Container
            builder.Services.AddSingleton<IConnectionMultiplexer>(Provider =>
            {
                var Connection = builder.Configuration.GetConnectionString("Redis");

                // Like UseSqlServer
                return ConnectionMultiplexer.Connect(Connection);
            });

            builder.Services.AddAuthentication(Options =>
            {
                // How To Authenticate
                // Will Authenticate By Jwt Base Not Cookie Base
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                // Will Return UnAuthorized() When UnAuthenticated User Make A Request
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(Options =>
            {
                // How To Verify The Jwt Token
                // Check The Token Not Expired
                Options.SaveToken = true;
                // Check The Request Protocol Is Https
                Options.RequireHttpsMetadata = true;
                // Check This Parameters In The Token
                Options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Jwt:ProviderBaseUrl"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:ConsumerBaseUrl"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
                };
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddSingleton<ICachService, CachService>();

            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddScoped<IBasketRepository, BasketRepository>();

            builder.Services.AddScoped<IBasketService, BasketService>();

            builder.Services.AddScoped<IAccountService, AccountService>();

            builder.Services.AddControllers();

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

            // For Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Make Update-Database Command To Apply Migration(s) Not Applied
            using(var Scope = app.Services.CreateScope())
            {
                var Context = Scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var IdentityContext = Scope.ServiceProvider.GetRequiredService<IdentityStoreDbContext>();

                try
                {
                    // Update-Database 
                    await Context.Database.MigrateAsync();
                    await IdentityContext.Database.MigrateAsync();

                    // Add Seed Data To DB For First Running
                    SeedDataToAppDbContext.Seed(Context);
                    SeedDataToIdentityDbContext.Seed(IdentityContext);
                }
                catch(Exception Ex)
                {
                    Console.WriteLine(Ex.Message);
                }
            }


            // Configure the HTTP request pipeline.

            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();

            app.UseAuthorization();

            // MiddleWare To Handle The Not Found EndPoint
            app.UseStatusCodePagesWithReExecute("/api/Errors");

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}
