FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Ergenekon.Host/Ergenekon.Host.csproj", "src/Ergenekon.Host/"]
COPY ["src/Ergenekon.Application/Ergenekon.Application.csproj", "src/Ergenekon.Application/"]
COPY ["src/Ergenekon.Domain/Ergenekon.Domain.csproj", "src/Ergenekon.Domain/"]
COPY ["src/Ergenekon.Infrastructure/Ergenekon.Infrastructure.csproj", "src/Ergenekon.Infrastructure/"]
RUN dotnet restore "./src/Ergenekon.Host/./Ergenekon.Host.csproj"
COPY . .
WORKDIR "/src/src/Ergenekon.Host"
RUN dotnet build "./Ergenekon.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Ergenekon.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ergenekon.Host.dll"]