using Microsoft.EntityFrameworkCore;
using PortalSaudeConectada.Core.Domain.Entities;
using PortalSaudeConectada.Core.Infrastructure.Data;

namespace PortalSaudeConectada.Core.Infrastructure.Repositories;

/// <summary>
/// Implementação do repositório de Refresh Tokens
/// </summary>
public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(PortalDbContext context)
        : base(context)
    {
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(rt => rt.Usuario)
            .FirstOrDefaultAsync(rt => rt.Token == token && !rt.IsDeleted, cancellationToken);
    }

    public async Task<List<RefreshToken>> GetActiveTokensByUserIdAsync(Guid usuarioId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(rt => rt.UsuarioId == usuarioId 
                && !rt.IsRevoked 
                && !rt.IsDeleted 
                && rt.ExpiresAt > DateTime.UtcNow)
            .OrderByDescending(rt => rt.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task RevokeTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        var refreshToken = await GetByTokenAsync(token, cancellationToken);
        
        if (refreshToken != null && !refreshToken.IsRevoked)
        {
            refreshToken.IsRevoked = true;
            refreshToken.RevokedAt = DateTime.UtcNow;
            await UpdateAsync(refreshToken, cancellationToken);
        }
    }

    public async Task RevokeAllUserTokensAsync(Guid usuarioId, CancellationToken cancellationToken = default)
    {
        var tokens = await GetActiveTokensByUserIdAsync(usuarioId, cancellationToken);
        
        foreach (var token in tokens)
        {
            token.IsRevoked = true;
            token.RevokedAt = DateTime.UtcNow;
        }

        if (tokens.Any())
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task CleanupExpiredTokensAsync(CancellationToken cancellationToken = default)
    {
        var expiredTokens = await _dbSet
            .Where(rt => rt.ExpiresAt < DateTime.UtcNow || rt.IsRevoked)
            .ToListAsync(cancellationToken);

        foreach (var token in expiredTokens)
        {
            token.IsDeleted = true;
            token.DeletedAt = DateTime.UtcNow;
        }

        if (expiredTokens.Any())
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

