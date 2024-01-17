
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SecondProject.CQRS;
using SecondProject.CQRS.Command.Create;
using SecondProject.CQRS.Command.Delete;
using SecondProject.CQRS.Command.Update;
using SecondProject.CQRS.Query.GetAll;
using SecondProject.CQRS.Query.GetById;
using SecondProject.CQRS.Query.GetByName;
using SecondProject.Data;
using SecondProject.Interfaces;
using SecondProject.Models;
using SecondProject.Repository;
using SecondProject.Services;
using SecondProject.strategy;
using SecondProject.Swagger;
using SecondProject.UoW;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
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
            builder.Services.AddScoped<IShippingStrategy, FreeShippingStrategy>();
            builder.Services.AddScoped<IShippingStrategy, LocalShippingStrategy>();
            builder.Services.AddScoped<IShippingStrategy, WorldwideShippingStrategy>();

            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            //it will register all the handlers in this assembly

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            builder.Services.AddScoped<IUser, IdentityUserServices>();
            builder.Services.AddScoped<JwtTokenService>();


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                //here we can set various options to costumize the identity service in our app
                options.User.RequireUniqueEmail = true; // this force that each user must have a uniqe address
            }).AddEntityFrameworkStores<AppDBContext>();


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                // Tell the authenticaion scheme "how/where" to validate the token + secret
                options.TokenValidationParameters = JwtTokenService.GetValidationPerameters(builder.Configuration);
            });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            //to enable token bearer functionality in swagger
            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

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
