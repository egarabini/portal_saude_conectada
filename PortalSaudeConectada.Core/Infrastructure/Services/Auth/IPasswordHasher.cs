namespace PortalSaudeConectada.Core.Infrastructure.Services.Auth;

/// <summary>
/// Interface para serviço de hash de senha
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Gera hash de uma senha
    /// </summary>
    string Hash(string password);

    /// <summary>
    /// Verifica se a senha corresponde ao hash
    /// </summary>
    bool Verify(string password, string hash);
}

