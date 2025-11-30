namespace PortalSaudeConectada.Shared.Dtos.Auth;

/// <summary>
/// Request para logout
/// </summary>
public class LogoutRequest
{
    /// <summary>
    /// Refresh token a ser invalidado (opcional)
    /// </summary>
    public string? RefreshToken { get; set; }
}

