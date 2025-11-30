namespace PortalSaudeConectada.Shared.Enums;

/// <summary>
/// Status do usuário no sistema
/// </summary>
public enum StatusUsuario
{
    /// <summary>
    /// Usuário ativo
    /// </summary>
    Ativo = 1,

    /// <summary>
    /// Usuário inativo
    /// </summary>
    Inativo = 2,

    /// <summary>
    /// Usuário bloqueado
    /// </summary>
    Bloqueado = 3,

    /// <summary>
    /// Aguardando ativação
    /// </summary>
    PendenteAtivacao = 4
}

