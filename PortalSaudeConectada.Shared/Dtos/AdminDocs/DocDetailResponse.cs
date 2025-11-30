namespace PortalSaudeConectada.Shared.Dtos.AdminDocs;

/// <summary>
/// Response com detalhes de um documento
/// </summary>
public class DocDetailResponse
{
    public string Projeto { get; set; } = string.Empty;
    public string Filepath { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string? CommitHash { get; set; }
    public List<DocVersionInfo> Versions { get; set; } = new();
}

/// <summary>
/// Informações de uma versão do documento
/// </summary>
public class DocVersionInfo
{
    public string Version { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string? CommitHash { get; set; }
}

