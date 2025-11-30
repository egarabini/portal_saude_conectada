using PortalSaudeConectada.Core.Application.Queries;
using PortalSaudeConectada.Shared.Contracts;
using BacklogResponseDto = PortalSaudeConectada.Shared.Dtos.AdminTeam.BacklogResponse;

namespace PortalSaudeConectada.Api.Features.AdminTeam.GetBacklog;

public sealed record GetBacklogQuery : IQuery<ApiResponse<BacklogResponseDto>>;

