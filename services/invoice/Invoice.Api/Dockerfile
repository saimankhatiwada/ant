FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/invoice/Invoice.Api/Invoice.Api.csproj", "services/invoice/Invoice.Api/"]
COPY ["services/invoice/Invoice.Application/Invoice.Application.csproj", "services/invoice/Invoice.Application/"]
COPY ["services/invoice/Invoice.Domain/Invoice.Domain.csproj", "services/invoice/Invoice.Domain/"]
COPY ["services/invoice/Invoice.Infrastructure/Invoice.Infrastructure.csproj", "services/invoice/Invoice.Infrastructure/"]
RUN dotnet restore "./services/invoice/Invoice.Api/./Invoice.Api.csproj"
COPY . .
WORKDIR "/src/services/invoice/Invoice.Api"
RUN dotnet build "./Invoice.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Invoice.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Invoice.Api.dll"]