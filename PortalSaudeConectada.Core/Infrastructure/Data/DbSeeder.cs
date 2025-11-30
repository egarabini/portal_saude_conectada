using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PortalSaudeConectada.Core.Domain.Entities;
using PortalSaudeConectada.Core.Infrastructure.Services.Auth;
using PortalSaudeConectada.Shared.Enums;

namespace PortalSaudeConectada.Core.Infrastructure.Data;

/// <summary>
/// Serviço para popular o banco de dados com dados iniciais
/// </summary>
public class DbSeeder
{
    private readonly PortalDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<DbSeeder> _logger;

    public DbSeeder(
        PortalDbContext context,
        IPasswordHasher passwordHasher,
        ILogger<DbSeeder> logger)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        try
        {
            // Verifica se já existem usuários
            if (await _context.Usuarios.AnyAsync())
            {
                _logger.LogInformation("Banco de dados já possui dados. Seed não será executado.");
                return;
            }

            _logger.LogInformation("Iniciando seed do banco de dados...");

            // Criar usuário admin
            var admin = new Usuario
            {
                Nome = "Administrador",
                Email = "admin@saudeconectada.com.br",
                Username = "admin",
                PasswordHash = _passwordHasher.Hash("Admin@123"),
                Cpf = "12345678901",
                Telefone = "(11) 98765-4321",
                Tipo = TipoUsuario.Administrador,
                Status = StatusUsuario.Ativo,
                EmailVerificado = true,
                CreatedBy = "System",
                CreatedAt = DateTime.UtcNow
            };

            // Criar usuário gestor
            var gestor = new Usuario
            {
                Nome = "Gestor de Saúde",
                Email = "gestor@saudeconectada.com.br",
                Username = "gestor",
                PasswordHash = _passwordHasher.Hash("Gestor@123"),
                Cpf = "98765432109",
                Telefone = "(11) 91234-5678",
                Tipo = TipoUsuario.Gestor,
                Status = StatusUsuario.Ativo,
                EmailVerificado = true,
                CreatedBy = "System",
                CreatedAt = DateTime.UtcNow
            };

            // Criar usuário profissional
            var profissional = new Usuario
            {
                Nome = "Profissional de Saúde",
                Email = "profissional@saudeconectada.com.br",
                Username = "profissional",
                PasswordHash = _passwordHasher.Hash("Prof@123"),
                Cpf = "11122233344",
                Telefone = "(11) 99999-8888",
                Tipo = TipoUsuario.Profissional,
                Status = StatusUsuario.Ativo,
                EmailVerificado = true,
                CreatedBy = "System",
                CreatedAt = DateTime.UtcNow
            };

            await _context.Usuarios.AddRangeAsync(admin, gestor, profissional);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Seed concluído com sucesso!");
            _logger.LogInformation("Usuários criados:");
            _logger.LogInformation("  - Admin: admin / Admin@123");
            _logger.LogInformation("  - Gestor: gestor / Gestor@123");
            _logger.LogInformation("  - Profissional: profissional / Prof@123");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao executar seed do banco de dados");
            throw;
        }
    }
}

