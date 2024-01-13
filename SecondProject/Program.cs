
using Microsoft.EntityFrameworkCore;
using SecondProject.CQRS;
using SecondProject.Data;
using SecondProject.Interfaces;
using SecondProject.strategy;
using SecondProject.UoW;
using System.Threading.Tasks;

namespace SecondProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // connect DB

            string connString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services
                .AddDbContext<AppDBContext>
                (opions => opions.UseSqlServer(connString));



            // Add services to the container.
            builder.Services.AddScoped<IUnitofWork, UnitOfwork>();

            builder.Services.AddScoped<IShippingContext, ShippingContext>();

            builder.Services.AddScoped<ProductCommandHandler>();

            builder.Services.AddScoped<ProductQueryHandler>();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
