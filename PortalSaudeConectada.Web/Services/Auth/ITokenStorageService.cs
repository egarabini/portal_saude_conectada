namespace PortalSaudeConectada.Web.Services.Auth;

/// <summary>
/// Serviço para armazenar e recuperar tokens de autenticação
/// </summary>
public interface ITokenStorageService
{
    /// <summary>
    /// Armazena o access token
    /// </summary>
    Task SetAccessTokenAsync(string token);

    /// <summary>
    /// Recupera o access token
    /// </summary>
    Task<string?> GetAccessTokenAsync();

    /// <summary>
    /// Armazena o refresh token
    /// </summary>
    Task SetRefreshTokenAsync(string token);

    /// <summary>
    /// Recupera o refresh token
    /// </summary>
    Task<string?> GetRefreshTokenAsync();

    /// <summary>
    /// Remove todos os tokens (logout)
    /// </summary>
    Task ClearTokensAsync();
}

