using FoodDelivery.Authorization;
using FoodDelivery.Authorization.Interface;
using FoodDelivery.DBAccess;
using FoodDelivery.Helpers;
using FoodDelivery.Repositories.ReadRepositories;
using FoodDelivery.Repositories.ReadRepositories.Interfaces;
using FoodDelivery.Repositories.WriteRepositories;
using FoodDelivery.Repositories.WriteRepositories.Interfaces;
using FoodDelivery.Services;
using FoodDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace FoodDelivery
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Available endpoints for testing FoodDelivery app", Version = "v1" });
            });

            services.AddAutoMapper(this.GetType().Assembly);

            // configure DI for repositories
            services.AddScoped<IReadFoodRepository, ReadFoodRepository>();
            services.AddScoped<IReadOrderRepository, ReadOrderRepository>();
            services.AddScoped<IReadRestaurantRepository, ReadRestaurantRepository>();
            services.AddScoped<IReadUserRepository, ReadUserRepository>();
            services.AddScoped<IWriteFoodRepository, WriteFoodRepository>();
            services.AddScoped<IWriteOrderRepository, WriteOrderRepository>();
            services.AddScoped<IWriteRestaurantRepository, WriteRestaurantRepository>();
            services.AddScoped<IWriteUserRepository, WriteUserRepository>();

            // configure strongly typed settings objects
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // configure DI for services
            services.AddTransient<IFoodService, FoodService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IRestaurantService, RestaurantService>();
            services.AddTransient<IUserService, UserService>();
            services.AddScoped<IJwtUtils, JwtUtils>();

            // configure database connection
            services.AddDbContext<FoodDeliveryContext>(options =>
            {
                options.UseInMemoryDatabase(Configuration.GetConnectionString("databaseName"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FoodDelivery v1"));
            }

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // global error handler
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            // custom jwt authorization middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
