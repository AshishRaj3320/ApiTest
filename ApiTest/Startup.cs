using ApiTest.Abstraction.Errors;
using ApiTest.Abstraction.Handlers;
using ApiTest.Abstraction.Middleware;
using ApiTest.Abstraction.Services;
using ApiTest.Abstraction.Services.Enum;
using ApiTest.SQL;
using ApiTest.SQL.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ApiTest
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

            services.AddScoped<IPaymentHandler, PaymentHandler>();

            //Automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<PaymentProcessContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient(typeof(IEntityRepository<>), typeof(EntityRepository<>));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage);

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            services.AddScoped<PremiumPaymentGateway>();
            services.AddScoped<ExpensivePaymentGateway>();
            services.AddScoped<CheapPaymentGateway>();

            services.AddTransient<Func<ServiceEnum, IPaymentGateway>>(serviceProvider => key =>
            {
                switch (key)
                {
                    case ServiceEnum.Premium:
                        return serviceProvider.GetService<PremiumPaymentGateway>();
                    case ServiceEnum.Expensive:
                        return serviceProvider.GetService<ExpensivePaymentGateway>();
                    case ServiceEnum.Cheap:
                        return serviceProvider.GetService<CheapPaymentGateway>();
                    default:
                        return null;
                }
            });

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PaymentProcessContext db)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            db.Database.EnsureCreated();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
