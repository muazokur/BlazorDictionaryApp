using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BlazorDictionary.Api.WebApi.Infrastructure.Extensions
{
    public static class AuthRegistiration
    {
        public static IServiceCollection ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var singinKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["AuthConfig:Secret"]));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = singinKey,
                };
            });

            return services;
        }
    }
}
