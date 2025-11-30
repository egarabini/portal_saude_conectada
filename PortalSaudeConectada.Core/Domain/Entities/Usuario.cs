using PortalSaudeConectada.Shared.Enums;

namespace PortalSaudeConectada.Core.Domain.Entities;

/// <summary>
/// Entidade de Usuário
/// </summary>
public class Usuario : BaseEntity
{
    /// <summary>
    /// Nome completo do usuário
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Email do usuário
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Username para login
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Hash da senha
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// CPF do usuário
    /// </summary>
    public string? Cpf { get; set; }

    /// <summary>
    /// Telefone do usuário
    /// </summary>
    public string? Telefone { get; set; }

    /// <summary>
    /// Tipo do usuário
    /// </summary>
    public TipoUsuario Tipo { get; set; }

    /// <summary>
    /// Status do usuário
    /// </summary>
    public StatusUsuario Status { get; set; }

    /// <summary>
    /// Data do último login
    /// </summary>
    public DateTime? UltimoLogin { get; set; }

    /// <summary>
    /// Indica se o email foi verificado
    /// </summary>
    public bool EmailVerificado { get; set; }

    /// <summary>
    /// Token de verificação de email
    /// </summary>
    public string? EmailVerificationToken { get; set; }

    /// <summary>
    /// Token de reset de senha
    /// </summary>
    public string? PasswordResetToken { get; set; }

    /// <summary>
    /// Data de expiração do token de reset de senha
    /// </summary>
    public DateTime? PasswordResetTokenExpires { get; set; }

    public Usuario()
    {
        Status = StatusUsuario.PendenteAtivacao;
        EmailVerificado = false;
    }
}

