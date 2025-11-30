using PortalSaudeConectada.Core.Application.Queries;
using PortalSaudeConectada.Shared.Contracts;
using SprintsResponseDto = PortalSaudeConectada.Shared.Dtos.AdminTeam.SprintsResponse;

namespace PortalSaudeConectada.Api.Features.AdminTeam.GetSprints;

public sealed record GetSprintsQuery : IQuery<ApiResponse<SprintsResponseDto>>;

