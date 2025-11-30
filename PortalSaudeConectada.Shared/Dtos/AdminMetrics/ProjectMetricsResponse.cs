namespace PortalSaudeConectada.Shared.Dtos.AdminMetrics;

/// <summary>
/// Response com métricas de um projeto específico
/// </summary>
public class ProjectMetricsResponse
{
    /// <summary>
    /// Nome do projeto
    /// </summary>
    public string Projeto { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp da consulta
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Lista de métricas do projeto
    /// </summary>
    public List<MetricDetail> Metrics { get; set; } = new();
}

/// <summary>
/// Detalhe de uma métrica
/// </summary>
public class MetricDetail
{
    /// <summary>
    /// Nome da métrica
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Valor da métrica
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp da métrica
    /// </summary>
    public DateTime Timestamp { get; set; }
}

