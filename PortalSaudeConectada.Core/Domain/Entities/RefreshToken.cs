namespace PortalSaudeConectada.Core.Domain.Entities;

/// <summary>
/// Entidade para armazenar refresh tokens
/// </summary>
public class RefreshToken : BaseEntity
{
    /// <summary>
    /// Token único gerado
    /// </summary>
    public required string Token { get; set; }

    /// <summary>
    /// ID do usuário dono do token
    /// </summary>
    public Guid UsuarioId { get; set; }

    /// <summary>
    /// Usuário dono do token
    /// </summary>
    public Usuario? Usuario { get; set; }

    /// <summary>
    /// Data de expiração do token
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Indica se o token foi revogado
    /// </summary>
    public bool IsRevoked { get; set; }

    /// <summary>
    /// Data de revogação do token
    /// </summary>
    public DateTime? RevokedAt { get; set; }

    /// <summary>
    /// IP do cliente que gerou o token
    /// </summary>
    public string? IpAddress { get; set; }

    /// <summary>
    /// User Agent do cliente que gerou o token
    /// </summary>
    public string? UserAgent { get; set; }

    /// <summary>
    /// Verifica se o token está ativo (não expirado e não revogado)
    /// </summary>
    public bool IsActive => !IsRevoked && !IsDeleted && ExpiresAt > DateTime.UtcNow;
}

