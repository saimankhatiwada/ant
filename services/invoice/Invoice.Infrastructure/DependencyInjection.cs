using Asp.Versioning;
using Invoice.Application.Abstractions.Authentication;
using Invoice.Application.Abstractions.Authorization;
using Invoice.Application.Abstractions.Clock;
using Invoice.Application.Abstractions.Data;
using Invoice.Domain.Abstractions;
using Invoice.Domain.Invoices;
using Invoice.Infrastructure.Athorization;
using Invoice.Infrastructure.Authentication;
using Invoice.Infrastructure.Clock;
using Invoice.Infrastructure.Data;
using Invoice.Infrastructure.Outbox;
using Invoice.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;

namespace Invoice.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        AddPersistence(services, configuration);

        AddAuthentication(services, configuration);

        AddAuthorization(services, configuration);

        AddHealthChecks(services, configuration);

        AddApiVersioning(services);

        AddBackgroundJobs(services, configuration);

        return services;
    }

    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Database") ??
                                  throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<InvoiceDbContext>(options =>
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention());

        services.AddScoped<IInvoiceRepository, InvoiceRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<InvoiceDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ =>
            new SqlConnectionFactory(connectionString));
    }

    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.Configure<AuthenticationOptions>(configuration.GetSection("Authentication"));

        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.AddHttpContextAccessor();

        services.AddScoped<ITokenContext, TokenContext>();
    }

    private static void AddAuthorization(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthorizeOptions>(configuration.GetSection(AuthorizeOptions.Name));

        services.AddHttpClient<IAuthorizeService, AuthorizeService>((serviceProvider, httpClient) => 
        {
            AuthorizeOptions authorizeOptions = serviceProvider.GetRequiredService<IOptions<AuthorizeOptions>>().Value;

            httpClient.BaseAddress = new Uri(authorizeOptions.BaseUrl);
        });

        services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();

        services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
    }

    private static void AddHealthChecks(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("Database")!)
            .AddUrlGroup(new Uri(configuration["KeyCloak:BaseUrl"]!), HttpMethod.Get, "keycloak");
    }

    private static void AddApiVersioning(IServiceCollection services)
    {
        services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
    }

    private static void AddBackgroundJobs(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OutboxOptions>(configuration.GetSection(OutboxOptions.Name));

        services.AddQuartz();

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        services.ConfigureOptions<ProcessOutboxMessagesJobSetup>();
    }
}
