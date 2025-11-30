namespace PortalSaudeConectada.Shared.Dtos.Public;

/// <summary>
/// Request para envio de contato
/// </summary>
public class ContatoRequest
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Telefone { get; set; }
    public string? Organizacao { get; set; }
    public string Mensagem { get; set; } = string.Empty;
}

