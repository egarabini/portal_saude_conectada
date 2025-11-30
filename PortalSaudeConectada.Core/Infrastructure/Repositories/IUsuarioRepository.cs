using PortalSaudeConectada.Core.Domain.Entities;

namespace PortalSaudeConectada.Core.Infrastructure.Repositories;

/// <summary>
/// Interface de repositório para Usuario
/// </summary>
public interface IUsuarioRepository : IRepository<Usuario>
{
    /// <summary>
    /// Obtém um usuário por email
    /// </summary>
    Task<Usuario?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtém um usuário por username
    /// </summary>
    Task<Usuario?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtém um usuário por CPF
    /// </summary>
    Task<Usuario?> GetByCpfAsync(string cpf, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica se um email já está em uso
    /// </summary>
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica se um username já está em uso
    /// </summary>
    Task<bool> UsernameExistsAsync(string username, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica se um CPF já está em uso
    /// </summary>
    Task<bool> CpfExistsAsync(string cpf, CancellationToken cancellationToken = default);
}

