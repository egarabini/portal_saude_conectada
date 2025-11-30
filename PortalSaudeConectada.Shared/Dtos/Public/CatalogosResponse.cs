namespace PortalSaudeConectada.Shared.Dtos.Public;

/// <summary>
/// Response com catálogo de projetos e features
/// </summary>
public class CatalogosResponse
{
    public List<ProjetoInfo> Projetos { get; set; } = new();
    public List<FeatureInfo> Features { get; set; } = new();
}

/// <summary>
/// Informações de um projeto
/// </summary>
public class ProjetoInfo
{
    public string Id { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
}

/// <summary>
/// Informações de uma feature
/// </summary>
public class FeatureInfo
{
    public string Id { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}

