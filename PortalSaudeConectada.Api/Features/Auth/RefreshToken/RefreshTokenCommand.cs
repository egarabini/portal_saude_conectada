using PortalSaudeConectada.Core.Application.Commands;
using PortalSaudeConectada.Shared.Contracts;
using RefreshResponseDto = PortalSaudeConectada.Shared.Dtos.Auth.RefreshResponse;

namespace PortalSaudeConectada.Api.Features.Auth.RefreshToken;

/// <summary>
/// Command para renovar token de acesso
/// </summary>
public sealed record RefreshTokenCommand(string RefreshToken) : ICommand<ApiResponse<RefreshResponseDto>>;

