namespace PortalSaudeConectada.Core.Domain.Entities;

/// <summary>
/// Classe base para todas as entidades do domínio
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Identificador único da entidade
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Data de criação do registro
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Usuário que criou o registro
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Data da última atualização do registro
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Usuário que atualizou o registro pela última vez
    /// </summary>
    public string? UpdatedBy { get; set; }

    /// <summary>
    /// Indica se o registro foi excluído (soft delete)
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Data de exclusão do registro
    /// </summary>
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// Usuário que excluiu o registro
    /// </summary>
    public string? DeletedBy { get; set; }

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        IsDeleted = false;
    }
}

