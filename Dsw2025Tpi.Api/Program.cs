using Dsw2025Tpi.Api.Middleware;
using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Interfaces;
using Dsw2025Tpi.Application.Mapper;
using Dsw2025Tpi.Application.Services;
using Dsw2025Tpi.Data;
using Dsw2025Tpi.Data.Repositories;
using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Dsw2025Tpi.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
      
        builder.Services.AddScoped<IRepository, EfRepository>();
      
        builder.Services.AddScoped<IEntityMapper<Product, ProductModel.ProductResponse>, ProductMapper>();
        builder.Services.AddScoped<IEntityMapper<Order, OrderModel.OrderResponse>, OrderMapper>();


        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHealthChecks();

        var dbServer = builder.Configuration["ConnectionSettings:Server"];
        var dbName = builder.Configuration["ConnectionSettings:Database"];

        var connectionString = $"Server={dbServer};Database={dbName};Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";

        // Configurar la cadena de conexión a la base de datos
        builder.Services.AddDbContext<Dsw2025TpiContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            options.UseSeeding((c, t) =>
            {
                ((Dsw2025TpiContext)c).Seedwork<Customer>("Sources\\customers.json");
            });
        });


        var app = builder.Build();

        app.UseMiddleware<ExceptionHandler>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        
        app.MapHealthChecks("/healthcheck");

        app.Run();
    }
}
