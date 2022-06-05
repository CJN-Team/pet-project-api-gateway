using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Pet.Project.Api.Gateway.API.Handlers;
using System.Text;

namespace Pet.Project.Api.Gateway.API.Extensions
{
    public static class ApiServices
    {
        public static IServiceCollection ApiServicesConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.OcelotServiceConfig();
            services.AuthenticationService(configuration);
            return services;
        }

        private static IServiceCollection AuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["PublicKey"])),
                    ClockSkew = new TimeSpan(0)
                };
            });
            return services;
        }

        private static IServiceCollection OcelotServiceConfig(this IServiceCollection services)
        {
            services.AddOcelot()
                .AddDelegatingHandler<EncodingHandler>(true);
            return services;
        }
    }
}