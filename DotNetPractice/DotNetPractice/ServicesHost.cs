using DotNetPractice.Backend.Services;
using DotNetPractice.Backend.Services.Interfaces;
using DotNetPractice.DataAccess;
using DotNetPractice.DataModel;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetPractice
{
    public static class ServicesHost
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddDbContext<NorthwindContext>();
            services.AddTransient<NorthwindDapperContext>();

            services.AddTransient<IEmployeesService, EmployeesService>();
            services.AddTransient<ICustomersService, CustomersService>();
            services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<IOrderDetailsService, OrderDetailsService>();
        }
    }
}
