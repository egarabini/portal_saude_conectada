namespace PortalSaudeConectada.Shared.Dtos.Auth;

/// <summary>
/// Request para refresh token
/// </summary>
public class RefreshRequest
{
    /// <summary>
    /// Token de refresh
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;
}

/// <summary>
/// Alias para compatibilidade
/// </summary>
[Obsolete("Use RefreshRequest instead")]
public class RefreshTokenRequest : RefreshRequest
{
}

