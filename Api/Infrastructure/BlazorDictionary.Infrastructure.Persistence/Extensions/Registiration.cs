using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Infrastructure.Persistence.Context;
using BlazorDictionary.Infrastructure.Persistence.Repositories;
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
        public static string ConnectionString { get; set; }
        public static IServiceCollection AddInfrastructureRegistiration(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<BlazorDictionaryContext>(conf =>
            {
                var ConnectionString = configuration["BlazorDictionaryConnectionStrings"].ToString();
                conf.UseSqlServer(ConnectionString, options =>
                {
                    options.EnableRetryOnFailure();
                });
            });

            //var seedData = new SeedData();

            //seedData.SeedAsync(configuration).GetAwaiter().GetResult();


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmailConfirmationRepository, EmailConfirmationRepository>();
            services.AddScoped<IEntryCommentRepository, EntryCommentRepository>();
            services.AddScoped<IEntryRepository, EntryRepository>();

            return services;
        }
    }
}
