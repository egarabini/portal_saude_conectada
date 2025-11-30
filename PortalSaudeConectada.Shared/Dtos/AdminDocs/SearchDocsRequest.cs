namespace PortalSaudeConectada.Shared.Dtos.AdminDocs;

/// <summary>
/// Request para busca de documentos
/// </summary>
public class SearchDocsRequest
{
    public string Query { get; set; } = string.Empty;
    public string? Projeto { get; set; }
    public int Limit { get; set; } = 20;
}

