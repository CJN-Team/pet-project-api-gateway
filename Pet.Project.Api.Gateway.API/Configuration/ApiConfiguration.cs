using Ocelot.Middleware;

namespace Pet.Project.Api.Gateway.API.Configuration
{
    public static class ApiConfiguration
    {
        public static IConfigurationBuilder ApiConfigurationService(this IConfigurationBuilder configuration)
        {
            configuration.AddJsonFile("ocelot.json", false, true);
            return configuration;
        }

        public static IApplicationBuilder ConfigureServices(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseOcelot().Wait();
            return app;
        }
    }
}