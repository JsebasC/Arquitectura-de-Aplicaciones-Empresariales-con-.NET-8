# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Pacagroup.Ecommerce.Services.WebApi/Pacagroup.Ecommerce.Services.WebApi.csproj", "Pacagroup.Ecommerce.Services.WebApi/"]
COPY ["Pacagroup.Ecommerce.Application.DTO/Pacagroup.Ecommerce.Application.DTO.csproj", "Pacagroup.Ecommerce.Application.DTO/"]
COPY ["Pacagroup.Ecommerce.Application.Interface/Pacagroup.Ecommerce.Application.Interface.csproj", "Pacagroup.Ecommerce.Application.Interface/"]
COPY ["Pacagroup.Ecommerce.Transversal.Common/Pacagroup.Ecommerce.Transversal.Common.csproj", "Pacagroup.Ecommerce.Transversal.Common/"]
COPY ["Pacagroup.Ecommerce.Application.Main/Pacagroup.Ecommerce.Application.Main.csproj", "Pacagroup.Ecommerce.Application.Main/"]
COPY ["Pacagroup.Ecommerce.Domain.Entity/Pacagroup.Ecommerce.Domain.Entity.csproj", "Pacagroup.Ecommerce.Domain.Entity/"]
COPY ["Pacagroup.Ecommerce.Domain.Interface/Pacagroup.Ecommerce.Domain.Interface.csproj", "Pacagroup.Ecommerce.Domain.Interface/"]
COPY ["Pacagroup.Ecommerce.Domain.Core/Pacagroup.Ecommerce.Domain.Core.csproj", "Pacagroup.Ecommerce.Domain.Core/"]
COPY ["Pacagroup.Ecommerce.Infrastructure.Interface/Pacagroup.Ecommerce.Infrastructure.Interface.csproj", "Pacagroup.Ecommerce.Infrastructure.Interface/"]
COPY ["Pacagroup.Ecommerce.Infrastructure.Data/Pacagroup.Ecommerce.Infrastructure.Data.csproj", "Pacagroup.Ecommerce.Infrastructure.Data/"]
COPY ["Pacagroup.Ecommerce.Infrastructure.Repository/Pacagroup.Ecommerce.Infrastructure.Repository.csproj", "Pacagroup.Ecommerce.Infrastructure.Repository/"]
COPY ["Pacagroup.Ecommerce.Transversal.Logging/Pacagroup.Ecommerce.Transversal.Logging.csproj", "Pacagroup.Ecommerce.Transversal.Logging/"]
COPY ["Pacagroup.Ecommerce.Transversal.Mapper/Pacagroup.Ecommerce.Transversal.Mapper.csproj", "Pacagroup.Ecommerce.Transversal.Mapper/"]
RUN dotnet restore "./Pacagroup.Ecommerce.Services.WebApi/Pacagroup.Ecommerce.Services.WebApi.csproj"
COPY . .
WORKDIR "/src/Pacagroup.Ecommerce.Services.WebApi"
RUN dotnet build "./Pacagroup.Ecommerce.Services.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Pacagroup.Ecommerce.Services.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pacagroup.Ecommerce.Services.WebApi.dll"]