namespace PortalSaudeConectada.Shared.Dtos.Public;

/// <summary>
/// Response com status do sistema
/// </summary>
public class StatusResponse
{
    public string Status { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public ServicesStatus? Services { get; set; }
}

/// <summary>
/// Status dos serviços
/// </summary>
public class ServicesStatus
{
    public string CarePlanner { get; set; } = string.Empty;
    public string ConectaRede { get; set; } = string.Empty;
    public string IntelliCareMcp { get; set; } = string.Empty;
}

