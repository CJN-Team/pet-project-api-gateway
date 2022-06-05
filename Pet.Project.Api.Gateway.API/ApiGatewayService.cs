using Pet.Project.Api.Gateway.API.Configuration;
using Pet.Project.Api.Gateway.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ApiServicesConfig(builder.Configuration);
builder.Configuration.ApiConfigurationService();

WebApplicationService.WebApplicationConfiguration(builder.Build()).Run();