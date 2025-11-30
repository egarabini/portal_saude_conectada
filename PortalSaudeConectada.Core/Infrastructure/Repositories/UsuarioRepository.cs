using Microsoft.EntityFrameworkCore;
using PortalSaudeConectada.Core.Domain.Entities;
using PortalSaudeConectada.Core.Infrastructure.Data;

namespace PortalSaudeConectada.Core.Infrastructure.Repositories;

/// <summary>
/// Implementação do repositório de Usuario
/// </summary>
public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(PortalDbContext context) : base(context)
    {
    }

    public async Task<Usuario?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(u => !u.IsDeleted)
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<Usuario?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(u => !u.IsDeleted)
            .FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
    }

    public async Task<Usuario?> GetByCpfAsync(string cpf, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(u => !u.IsDeleted)
            .FirstOrDefaultAsync(u => u.Cpf == cpf, cancellationToken);
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(u => !u.IsDeleted)
            .AnyAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<bool> UsernameExistsAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(u => !u.IsDeleted)
            .AnyAsync(u => u.Username == username, cancellationToken);
    }

    public async Task<bool> CpfExistsAsync(string cpf, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(u => !u.IsDeleted)
            .AnyAsync(u => u.Cpf == cpf, cancellationToken);
    }
}

