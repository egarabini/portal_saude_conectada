using PortalSaudeConectada.Core.Application.Queries;
using PortalSaudeConectada.Shared.Contracts;
using CatalogosResponseDto = PortalSaudeConectada.Shared.Dtos.Public.CatalogosResponse;

namespace PortalSaudeConectada.Api.Features.Public.GetCatalogos;

public sealed record GetCatalogosQuery : IQuery<ApiResponse<CatalogosResponseDto>>;

