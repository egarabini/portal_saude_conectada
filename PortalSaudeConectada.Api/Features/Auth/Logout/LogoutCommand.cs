using PortalSaudeConectada.Core.Application.Commands;
using PortalSaudeConectada.Shared.Contracts;
using LogoutResponseDto = PortalSaudeConectada.Shared.Dtos.Auth.LogoutResponse;

namespace PortalSaudeConectada.Api.Features.Auth.Logout;

/// <summary>
/// Command para realizar logout
/// </summary>
public sealed record LogoutCommand(string? RefreshToken) : ICommand<ApiResponse<LogoutResponseDto>>;

