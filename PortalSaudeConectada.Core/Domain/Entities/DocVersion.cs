namespace PortalSaudeConectada.Core.Domain.Entities;

/// <summary>
/// Versionamento de documentação
/// </summary>
public class DocVersion : BaseEntity
{
    /// <summary>
    /// Nome do projeto
    /// </summary>
    public string Project { get; set; } = string.Empty;

    /// <summary>
    /// Caminho do arquivo
    /// </summary>
    public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// Versão do documento
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Conteúdo do documento
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Autor da versão
    /// </summary>
    public string? Author { get; set; }

    /// <summary>
    /// Hash do commit (Git)
    /// </summary>
    public string? CommitHash { get; set; }
}

