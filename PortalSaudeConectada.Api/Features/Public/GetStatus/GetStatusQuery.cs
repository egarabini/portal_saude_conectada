using PortalSaudeConectada.Core.Application.Queries;
using PortalSaudeConectada.Shared.Contracts;
using StatusResponseDto = PortalSaudeConectada.Shared.Dtos.Public.StatusResponse;

namespace PortalSaudeConectada.Api.Features.Public.GetStatus;

public sealed record GetStatusQuery : IQuery<ApiResponse<StatusResponseDto>>;

