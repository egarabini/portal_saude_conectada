namespace PortalSaudeConectada.Shared.Dtos.AdminDocs;

/// <summary>
/// Response com resultados da busca
/// </summary>
public class SearchDocsResponse
{
    public string Query { get; set; } = string.Empty;
    public int Total { get; set; }
    public List<object> Results { get; set; } = new();
}

