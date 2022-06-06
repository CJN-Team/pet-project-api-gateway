using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Pet.Project.Api.Gateway.API.Dtos;
using Pet.Project.Api.Gateway.API.Handlers;
using System.Security.Claims;

namespace Pet.Project.Api.Gateway.API.Extensions
{
    public static class ApiServices
    {
        public static IServiceCollection ApiServicesConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.OcelotServiceConfig();
            services.AuthenticationService(configuration);
            services.AuthorizationService(configuration);
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
            return services;
        }

        private static IServiceCollection AuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.Authority = $"https://{configuration["Auth0:Domain"]}/";
                options.Audience = configuration["Auth0:Audience"];
                options.TokenValidationParameters = new TokenValidationParameters { NameClaimType = ClaimTypes.NameIdentifier };
            });
            return services;
        }

        private static IServiceCollection AuthorizationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "read:messages",
                    policy => policy.Requirements.Add(
                        new HasScopeRequirement("read:messages", $"https://{configuration["Auth0:Domain"]}/")
                        )
                );
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