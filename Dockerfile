#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Pet.Project.Api.Gateway.API/Pet.Project.Api.Gateway.API.csproj", "Pet.Project.Api.Gateway.API/"]
COPY ["Pet.Project.Api.Gateway.Domain/Pet.Project.Api.Gateway.Domain.csproj", "Pet.Project.Api.Gateway.Domain/"]
COPY ["Pet.Project.Api.Gateway.Infraestructure/Pet.Project.Api.Gateway.Infraestructure.csproj", "Pet.Project.Api.Gateway.Infraestructure/"]
RUN dotnet restore "Pet.Project.Api.Gateway.API/Pet.Project.Api.Gateway.API.csproj"
COPY . .
WORKDIR "/src/Pet.Project.Api.Gateway.API"
RUN dotnet build "Pet.Project.Api.Gateway.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pet.Project.Api.Gateway.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pet.Project.Api.Gateway.API.dll"]