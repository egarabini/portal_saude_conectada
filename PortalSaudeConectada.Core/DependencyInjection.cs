using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PortalSaudeConectada.Core.Infrastructure.Data;
using PortalSaudeConectada.Core.Infrastructure.Repositories;
using PortalSaudeConectada.Core.Infrastructure.Services.Auth;

namespace PortalSaudeConectada.Core;

/// <summary>
/// Configuração de Dependency Injection do Core
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adiciona os serviços do Core ao container de DI
    /// </summary>
    public static IServiceCollection AddCoreServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // DbContext
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' não configurada.");

        services.AddDbContext<PortalDbContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsHistoryTable("__ef_migrations_history", "saude_conectada");
                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorCodesToAdd: null);
            });

            // Habilitar logs sensíveis apenas em Development
            if (configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            }
        });

        // Repositories
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        // Services de Autenticação
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        // Configurações JWT
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

        // Database Seeder
        services.AddScoped<DbSeeder>();

        // AutoMapper (será configurado posteriormente)
        // services.AddAutoMapper(typeof(DependencyInjection).Assembly);

        return services;
    }
}

