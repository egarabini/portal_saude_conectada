using PortalSaudeConectada.Core.Domain.Entities;

namespace PortalSaudeConectada.Core.Infrastructure.Repositories;

/// <summary>
/// Interface do repositório de Refresh Tokens
/// </summary>
public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
    /// <summary>
    /// Busca um refresh token ativo pelo token
    /// </summary>
    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca todos os refresh tokens ativos de um usuário
    /// </summary>
    Task<List<RefreshToken>> GetActiveTokensByUserIdAsync(Guid usuarioId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Revoga um refresh token
    /// </summary>
    Task RevokeTokenAsync(string token, CancellationToken cancellationToken = default);

    /// <summary>
    /// Revoga todos os refresh tokens de um usuário
    /// </summary>
    Task RevokeAllUserTokensAsync(Guid usuarioId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove tokens expirados do banco de dados
    /// </summary>
    Task CleanupExpiredTokensAsync(CancellationToken cancellationToken = default);
}

