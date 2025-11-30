namespace PortalSaudeConectada.Shared.Contracts;

/// <summary>
/// Resposta padrão da API
/// </summary>
/// <typeparam name="T">Tipo do dado retornado</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// Indica se a operação foi bem-sucedida
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Mensagem descritiva do resultado
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Dados retornados pela operação
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Lista de erros de validação (se houver)
    /// </summary>
    public List<ValidationError>? Errors { get; set; }

    /// <summary>
    /// Timestamp da resposta
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Cria uma resposta de sucesso
    /// </summary>
    public static ApiResponse<T> Ok(T data, string? message = null)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }

    /// <summary>
    /// Cria uma resposta de erro
    /// </summary>
    public static ApiResponse<T> Fail(string message, List<ValidationError>? errors = null)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Errors = errors
        };
    }

    /// <summary>
    /// Cria uma resposta de erro de validação
    /// </summary>
    public static ApiResponse<T> ValidationFail(List<ValidationError> errors)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = "Erro de validação",
            Errors = errors
        };
    }
}

/// <summary>
/// Resposta padrão sem dados
/// </summary>
public class ApiResponse : ApiResponse<object>
{
    /// <summary>
    /// Cria uma resposta de sucesso sem dados
    /// </summary>
    public static ApiResponse Ok(string? message = null)
    {
        return new ApiResponse
        {
            Success = true,
            Message = message
        };
    }

    /// <summary>
    /// Cria uma resposta de erro sem dados
    /// </summary>
    public new static ApiResponse Fail(string message, List<ValidationError>? errors = null)
    {
        return new ApiResponse
        {
            Success = false,
            Message = message,
            Errors = errors
        };
    }
}

