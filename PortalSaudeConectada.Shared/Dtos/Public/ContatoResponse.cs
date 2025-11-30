namespace PortalSaudeConectada.Shared.Dtos.Public;

/// <summary>
/// Response do envio de contato
/// </summary>
public class ContatoResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Protocolo { get; set; } = string.Empty;
}

