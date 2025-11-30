namespace PortalSaudeConectada.Shared.Contracts;

/// <summary>
/// Representa um erro de validação
/// </summary>
public class ValidationError
{
    /// <summary>
    /// Nome do campo que falhou na validação
    /// </summary>
    public string Field { get; set; } = string.Empty;

    /// <summary>
    /// Mensagem de erro
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Código do erro (opcional)
    /// </summary>
    public string? Code { get; set; }

    public ValidationError()
    {
    }

    public ValidationError(string field, string message, string? code = null)
    {
        Field = field;
        Message = message;
        Code = code;
    }
}

