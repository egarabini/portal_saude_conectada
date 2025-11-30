namespace PortalSaudeConectada.Shared.Dtos.AdminTeam;

/// <summary>
/// Response com backlog
/// </summary>
public class BacklogResponse
{
    public int Total { get; set; }
    public List<BacklogItem> Items { get; set; } = new();
}

/// <summary>
/// Item do backlog
/// </summary>
public class BacklogItem
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Projeto { get; set; } = string.Empty;
    public string Prioridade { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}

