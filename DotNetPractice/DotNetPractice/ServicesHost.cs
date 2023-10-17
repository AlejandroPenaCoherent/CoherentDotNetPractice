using DotNetPractice.Backend.Services;
using DotNetPractice.Backend.Services.Interfaces;
using DotNetPractice.DataAccess;
using DotNetPractice.DataModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetPractice
{
    public static class ServicesHost
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // data context's
            services.AddDbContext<NorthwindContext>();
            services.AddTransient<NorthwindDapperContext>();

            // services
            services.AddTransient<IEmployeesService, EmployeesService>();
            services.AddTransient<ICustomersService, CustomersService>();
            services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<IOrderDetailsService, OrderDetailsService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
        }

        public static void ConfigureIdentityAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var apiUrl = configuration.GetValue<string>("ApiUrl");

            services
                .AddAuthentication("Bearer")
                .AddIdentityServerAuthentication("Bearer", options =>
                {
                    options.Authority = apiUrl;
                    options.ApiName = "NorthwindAPI";
                    options.RequireHttpsMetadata = false;
                });
        }
    }
}
