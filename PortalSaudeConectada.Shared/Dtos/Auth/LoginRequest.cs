namespace PortalSaudeConectada.Shared.Dtos.Auth;

/// <summary>
/// Request para login
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Email ou username do usuário
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Senha do usuário
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Indica se deve manter o usuário logado
    /// </summary>
    public bool RememberMe { get; set; }
}

