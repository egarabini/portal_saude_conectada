using PortalSaudeConectada.Core.Application.Commands;
using PortalSaudeConectada.Shared.Contracts;
using LogoutResponseDto = PortalSaudeConectada.Shared.Dtos.Auth.LogoutResponse;

namespace PortalSaudeConectada.Api.Features.Auth.Logout;

/// <summary>
/// Handler para processar o comando de logout
/// </summary>
public sealed class LogoutCommandHandler : ICommandHandler<LogoutCommand, ApiResponse<LogoutResponseDto>>
{
    private readonly ILogger<LogoutCommandHandler> _logger;

    public LogoutCommandHandler(ILogger<LogoutCommandHandler> logger)
    {
        _logger = logger;
    }

    public async Task<ApiResponse<LogoutResponseDto>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Tentativa de logout");

        // TODO: Integrar com Keycloak
        // TODO: Invalidar refresh token no banco de dados
        // TODO: Adicionar token à blacklist

        // Implementação temporária para demonstração
        await Task.Delay(100, cancellationToken); // Simula operação assíncrona

        var response = new LogoutResponseDto
        {
            Success = true
        };

        _logger.LogInformation("Logout bem-sucedido");
        return ApiResponse<LogoutResponseDto>.Ok(response, "Logout realizado com sucesso");
    }
}

