using Microsoft.Extensions.Options;
using PortalSaudeConectada.Core.Application.Commands;
using PortalSaudeConectada.Core.Infrastructure.Repositories;
using PortalSaudeConectada.Core.Infrastructure.Services.Auth;
using PortalSaudeConectada.Shared.Contracts;
using RefreshResponseDto = PortalSaudeConectada.Shared.Dtos.Auth.RefreshResponse;

namespace PortalSaudeConectada.Api.Features.Auth.RefreshToken;

/// <summary>
/// Handler para processar o comando de refresh token
/// </summary>
public sealed class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, ApiResponse<RefreshResponseDto>>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly JwtSettings _jwtSettings;
    private readonly ILogger<RefreshTokenCommandHandler> _logger;

    public RefreshTokenCommandHandler(
        IRefreshTokenRepository refreshTokenRepository,
        IJwtTokenService jwtTokenService,
        IOptions<JwtSettings> jwtSettings,
        ILogger<RefreshTokenCommandHandler> logger)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _jwtTokenService = jwtTokenService;
        _jwtSettings = jwtSettings.Value;
        _logger = logger;
    }

    public async Task<ApiResponse<RefreshResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Tentativa de refresh token");

        // Buscar refresh token no banco
        var refreshToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken, cancellationToken);

        if (refreshToken == null)
        {
            _logger.LogWarning("Refresh token não encontrado");
            return ApiResponse<RefreshResponseDto>.Fail("Refresh token inválido");
        }

        // Verificar se o token está ativo
        if (!refreshToken.IsActive)
        {
            _logger.LogWarning("Refresh token inativo ou expirado. UsuarioId: {UsuarioId}", refreshToken.UsuarioId);
            return ApiResponse<RefreshResponseDto>.Fail("Refresh token inválido ou expirado");
        }

        // Verificar se o usuário existe e está ativo
        if (refreshToken.Usuario == null || refreshToken.Usuario.Status != Shared.Enums.StatusUsuario.Ativo)
        {
            _logger.LogWarning("Usuário inativo ou não encontrado. UsuarioId: {UsuarioId}", refreshToken.UsuarioId);
            return ApiResponse<RefreshResponseDto>.Fail("Usuário inativo");
        }

        // Gerar novo access token
        var newAccessToken = _jwtTokenService.GenerateAccessToken(refreshToken.Usuario);

        var response = new RefreshResponseDto
        {
            AccessToken = newAccessToken,
            ExpiresIn = _jwtSettings.ExpirationMinutes * 60
        };

        _logger.LogInformation("Refresh token bem-sucedido para UsuarioId: {UsuarioId}", refreshToken.UsuarioId);
        return ApiResponse<RefreshResponseDto>.Ok(response, "Token renovado com sucesso");
    }
}

