using Ocelot.Middleware;

namespace Pet.Project.Api.Gateway.API.Extensions
{
    public static class WebApplicationService
    {
        public static WebApplication WebApplicationConfiguration(WebApplication application)
        {
            application.UseOcelot().Wait();
            return application;
        }
    }
}