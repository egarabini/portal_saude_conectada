using PortalSaudeConectada.Shared.Dtos.Auth;

namespace PortalSaudeConectada.Web.Services.Auth;

/// <summary>
/// Serviço de autenticação para o Blazor
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Realiza login do usuário
    /// </summary>
    Task<LoginResponse?> LoginAsync(string username, string password);

    /// <summary>
    /// Realiza logout do usuário
    /// </summary>
    Task LogoutAsync();

    /// <summary>
    /// Renova o access token usando o refresh token
    /// </summary>
    Task<RefreshResponse?> RefreshTokenAsync();

    /// <summary>
    /// Verifica se o usuário está autenticado
    /// </summary>
    Task<bool> IsAuthenticatedAsync();

    /// <summary>
    /// Obtém as informações do usuário atual
    /// </summary>
    Task<UserInfo?> GetCurrentUserAsync();
}

