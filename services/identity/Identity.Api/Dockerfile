FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 7000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/identity/Identity.Api/Identity.Api.csproj", "services/identity/Identity.Api/"]
COPY ["services/identity/Identity.Application/Identity.Application.csproj", "services/identity/Identity.Application/"]
COPY ["services/identity/Identity.Domain/Identity.Domain.csproj", "services/identity/Identity.Domain/"]
COPY ["services/identity/Identity.Infrastructure/Identity.Infrastructure.csproj", "services/identity/Identity.Infrastructure/"]
RUN dotnet restore "./services/identity/Identity.Api/./Identity.Api.csproj"
COPY . .
WORKDIR "/src/services/identity/Identity.Api"
RUN dotnet build "./Identity.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Identity.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.Api.dll"]