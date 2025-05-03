
using Abstraction;
using Domain.Contracts;
using E_Commerce.Web.CustomMiddlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Repositories;
using Services;
using Shared.ErrorModels;
using StackExchange.Redis;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region DI Container Services
            // Add services to the container.

            builder.Services.AddControllers(); //DI Api
            builder.Services.AddDbContext<StoreDBContext>(options=>
            {
                var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(ConnectionString);
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IDbInitializer, DbInitializer>();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

            builder.Services.AddAutoMapper(typeof(AssemblyReferences).Assembly);
            builder.Services.AddScoped<IServicesManger, ServicesManger>();

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var Errors = context.ModelState.Where(M => M.Value.Errors.Any())
                                .Select(M => new ValidationError()
                                {
                                    Field = M.Key,
                                    Errors = M.Value.Errors.Select(E => E.ErrorMessage)
                                });
                    //--------------------------------------------
                    var Response = new ValidationErrorToReturn()
                    {
                        ValidationErrors = Errors
                    };
                    return new BadRequestObjectResult(Response);
                };

            });

            builder.Services.AddScoped<IBasketRepository,BasketRepository>();
            builder.Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnectionString"));

            });

            
            #endregion

            var app = builder.Build();

            await InitializeDbAsync(app);


            #region  MiddleWares -Configire Piplines
            // Configure the HTTP request pipeline.
            app.UseMiddleware<CustomExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
  
            app.UseAuthorization();


            app.MapControllers();
            #endregion

            app.Run();
            
        }


        public static async Task InitializeDbAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dBInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dBInitializer.InitializerAsync();
        }
    }
}
