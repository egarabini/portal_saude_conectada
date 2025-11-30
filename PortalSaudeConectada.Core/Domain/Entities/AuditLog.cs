namespace PortalSaudeConectada.Core.Domain.Entities;

/// <summary>
/// Log de auditoria para ações no sistema
/// </summary>
public class AuditLog : BaseEntity
{
    /// <summary>
    /// ID do usuário que executou a ação
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// Ação executada
    /// </summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// Recurso afetado
    /// </summary>
    public string? Resource { get; set; }

    /// <summary>
    /// Detalhes da ação (JSON)
    /// </summary>
    public string? Details { get; set; }

    /// <summary>
    /// Endereço IP de origem
    /// </summary>
    public string? IpAddress { get; set; }
}

