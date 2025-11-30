namespace PortalSaudeConectada.Core.Domain.Entities;

/// <summary>
/// Cache de métricas agregadas por projeto
/// </summary>
public class CachedMetric : BaseEntity
{
    /// <summary>
    /// Nome do projeto
    /// </summary>
    public string Project { get; set; } = string.Empty;

    /// <summary>
    /// Nome da métrica
    /// </summary>
    public string MetricName { get; set; } = string.Empty;

    /// <summary>
    /// Valor da métrica (JSON)
    /// </summary>
    public string MetricValue { get; set; } = string.Empty;

    /// <summary>
    /// Data de expiração do cache
    /// </summary>
    public DateTime? ExpiresAt { get; set; }
}

