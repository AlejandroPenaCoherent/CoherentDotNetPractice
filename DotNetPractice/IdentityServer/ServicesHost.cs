using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace IdentityServer
{
    public static class ServicesHost
    {
        public static void ConfigureIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            var assemblyName = typeof(Program).Assembly.GetName().Name;
            var connectionString = configuration.GetConnectionString("Northwind");

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = opt => opt.UseSqlServer(connectionString, contextOpt => contextOpt.MigrationsAssembly(assemblyName));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = opt => opt.UseSqlServer(connectionString, contextOpt => contextOpt.MigrationsAssembly(assemblyName));
                });
        }

        public static void EnsureIdentitySeed(this IApplicationBuilder app, IConfiguration configuration)
        {
            var seedCreationNeeded = configuration.GetValue<bool>("seed");

            if (!seedCreationNeeded)
            {
                return;
            }

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                context.Database.Migrate();

                if (!context.Clients.Any())
                {
                    foreach (var client in Config.Clients)
                    {
                        context.Clients.Add(client.ToEntity());
                    }

                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.ApiResources)
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }

                    context.SaveChanges();
                }

                if (!context.ApiScopes.Any())
                {
                    foreach (var resource in Config.ApiScopes)
                    {
                        context.ApiScopes.Add(resource.ToEntity());
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
