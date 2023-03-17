using BlazorDictionary.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Infrastructure.Persistence.Extensions
{
    public static class Registiration
    {
        public static IServiceCollection AddInfrastructureRegistiration(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<BlazorDictionaryContext>(conf =>
            {
                var connectionString = configuration["BlazorDictionaryConnectionStrings"].ToString();
                conf.UseSqlServer(connectionString, options =>
                {
                    options.EnableRetryOnFailure();
                });
            });

            //var seedData = new SeedData();

            //seedData.SeedAsync(configuration).GetAwaiter().GetResult();


            return services;
        }
    }
}
