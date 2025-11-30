using PortalSaudeConectada.Core.Application.Queries;
using PortalSaudeConectada.Shared.Contracts;
using DocDetailResponseDto = PortalSaudeConectada.Shared.Dtos.AdminDocs.DocDetailResponse;

namespace PortalSaudeConectada.Api.Features.AdminDocs.GetDoc;

public sealed record GetDocQuery(string Projeto, string Filepath) : IQuery<ApiResponse<DocDetailResponseDto>>;

