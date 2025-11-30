namespace PortalSaudeConectada.Shared.Dtos.AdminDocs;

/// <summary>
/// Response com lista de documentos
/// </summary>
public class DocsListResponse
{
    public int Total { get; set; }
    public List<DocInfo> Docs { get; set; } = new();
}

/// <summary>
/// Informações de um documento
/// </summary>
public class DocInfo
{
    public string Projeto { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string Titulo { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
}

