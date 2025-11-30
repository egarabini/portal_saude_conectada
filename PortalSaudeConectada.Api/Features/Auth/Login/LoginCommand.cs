using PortalSaudeConectada.Core.Application.Commands;
using PortalSaudeConectada.Shared.Contracts;
using LoginResponseDto = PortalSaudeConectada.Shared.Dtos.Auth.LoginResponse;

namespace PortalSaudeConectada.Api.Features.Auth.Login;

/// <summary>
/// Command para realizar login de usuário
/// </summary>
public sealed record LoginCommand(string Username, string Password) : ICommand<ApiResponse<LoginResponseDto>>;

