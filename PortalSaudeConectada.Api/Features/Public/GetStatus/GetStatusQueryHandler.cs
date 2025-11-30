using PortalSaudeConectada.Core.Application.Queries;
using PortalSaudeConectada.Shared.Contracts;
using StatusResponseDto = PortalSaudeConectada.Shared.Dtos.Public.StatusResponse;
using ServicesStatusDto = PortalSaudeConectada.Shared.Dtos.Public.ServicesStatus;

namespace PortalSaudeConectada.Api.Features.Public.GetStatus;

public sealed class GetStatusQueryHandler : IQueryHandler<GetStatusQuery, ApiResponse<StatusResponseDto>>
{
    private readonly ILogger<GetStatusQueryHandler> _logger;

    public GetStatusQueryHandler(ILogger<GetStatusQueryHandler> logger)
    {
        _logger = logger;
    }

    public async Task<ApiResponse<StatusResponseDto>> Handle(GetStatusQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Obtendo status do sistema");

        await Task.CompletedTask; // Simula operação assíncrona

        var response = new StatusResponseDto
        {
            Status = "healthy",
            Version = "1.0.0",
            Timestamp = DateTime.UtcNow,
            Services = new ServicesStatusDto
            {
                CarePlanner = "operational",
                ConectaRede = "operational",
                IntelliCareMcp = "operational"
            }
        };

        _logger.LogInformation("Status obtido: {Status}", response.Status);
        return ApiResponse<StatusResponseDto>.Ok(response);
    }
}

