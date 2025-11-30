namespace PortalSaudeConectada.Shared.Dtos.Auth;

/// <summary>
/// Response para refresh token
/// </summary>
public class RefreshResponse
{
    /// <summary>
    /// Novo token de acesso
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// Tipo do token (Bearer)
    /// </summary>
    public string TokenType { get; set; } = "Bearer";

    /// <summary>
    /// Tempo de expiração em segundos
    /// </summary>
    public int ExpiresIn { get; set; }
}

