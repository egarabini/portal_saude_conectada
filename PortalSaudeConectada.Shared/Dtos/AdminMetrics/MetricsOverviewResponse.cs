namespace PortalSaudeConectada.Shared.Dtos.AdminMetrics;

/// <summary>
/// Response com visão geral das métricas
/// </summary>
public class MetricsOverviewResponse
{
    /// <summary>
    /// Timestamp da consulta
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Lista de projetos com suas métricas
    /// </summary>
    public List<ProjectMetricsSummary> Projetos { get; set; } = new();

    /// <summary>
    /// Quantidade de métricas em cache
    /// </summary>
    public int Cached { get; set; }
}

/// <summary>
/// Resumo das métricas de um projeto
/// </summary>
public class ProjectMetricsSummary
{
    /// <summary>
    /// ID do projeto
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Nome do projeto
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Status do projeto (operational, development, maintenance)
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Uptime do projeto
    /// </summary>
    public string Uptime { get; set; } = string.Empty;

    /// <summary>
    /// Progresso do checklist
    /// </summary>
    public int ChecklistProgress { get; set; }

    /// <summary>
    /// Total de itens do checklist
    /// </summary>
    public int ChecklistTotal { get; set; }

    /// <summary>
    /// Número de alertas ativos
    /// </summary>
    public int Alerts { get; set; }
}

