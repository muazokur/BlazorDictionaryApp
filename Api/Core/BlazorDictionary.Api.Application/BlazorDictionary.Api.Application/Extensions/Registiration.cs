using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Api.Application.Extensions
{
    public static class Registiration
    {
        public static IServiceCollection AddApplicationRegistiration(this IServiceCollection services)
        {
            var assm=Assembly.GetExecutingAssembly();

            services.AddMediatR(assm);
            services.AddAutoMapper(assm);
            services.AddValidatorsFromAssembly(assm);

            return services;
        }
    }
}
