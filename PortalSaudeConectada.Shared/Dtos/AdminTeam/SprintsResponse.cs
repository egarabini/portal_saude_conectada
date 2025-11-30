namespace PortalSaudeConectada.Shared.Dtos.AdminTeam;

/// <summary>
/// Response com sprints
/// </summary>
public class SprintsResponse
{
    public SprintDetail? SprintAtual { get; set; }
    public List<SprintSummary> Sprints { get; set; } = new();
}

/// <summary>
/// Detalhes de uma sprint
/// </summary>
public class SprintDetail
{
    public string Id { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public DateTime Inicio { get; set; }
    public DateTime Fim { get; set; }
    public int Progresso { get; set; }
    public int ItensCompletos { get; set; }
    public int ItensTotal { get; set; }
}

/// <summary>
/// Resumo de uma sprint
/// </summary>
public class SprintSummary
{
    public string Id { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int Progresso { get; set; }
}

