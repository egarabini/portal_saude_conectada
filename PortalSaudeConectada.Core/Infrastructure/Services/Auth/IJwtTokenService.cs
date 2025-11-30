using System.Security.Claims;
using PortalSaudeConectada.Core.Domain.Entities;

namespace PortalSaudeConectada.Core.Infrastructure.Services.Auth;

/// <summary>
/// Interface para serviço de geração e validação de JWT tokens
/// </summary>
public interface IJwtTokenService
{
    /// <summary>
    /// Gera um access token JWT para o usuário
    /// </summary>
    string GenerateAccessToken(Usuario usuario);

    /// <summary>
    /// Gera um refresh token aleatório
    /// </summary>
    string GenerateRefreshToken();

    /// <summary>
    /// Valida um token JWT e retorna os claims
    /// </summary>
    ClaimsPrincipal? ValidateToken(string token);
}

