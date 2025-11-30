namespace PortalSaudeConectada.Shared.Dtos.Auth;

/// <summary>
/// Response do login
/// </summary>
public class LoginResponse
{
    /// <summary>
    /// Token de acesso JWT
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// Token de refresh
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;

    /// <summary>
    /// Tipo do token (geralmente "Bearer")
    /// </summary>
    public string TokenType { get; set; } = "Bearer";

    /// <summary>
    /// Tempo de expiração do token em segundos
    /// </summary>
    public int ExpiresIn { get; set; }

    /// <summary>
    /// Informações do usuário logado
    /// </summary>
    public UserInfo? User { get; set; }
}

/// <summary>
/// Informações básicas do usuário
/// </summary>
public class UserInfo
{
    public string Id { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Name { get; set; }
    public List<string> Roles { get; set; } = new();
}

