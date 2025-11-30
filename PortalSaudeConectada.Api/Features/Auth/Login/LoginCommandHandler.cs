using Microsoft.Extensions.Options;
using PortalSaudeConectada.Core.Application.Commands;
using PortalSaudeConectada.Core.Domain.Entities;
using PortalSaudeConectada.Core.Infrastructure.Repositories;
using PortalSaudeConectada.Core.Infrastructure.Services.Auth;
using PortalSaudeConectada.Shared.Contracts;
using LoginResponseDto = PortalSaudeConectada.Shared.Dtos.Auth.LoginResponse;
using UserInfoDto = PortalSaudeConectada.Shared.Dtos.Auth.UserInfo;

namespace PortalSaudeConectada.Api.Features.Auth.Login;

/// <summary>
/// Handler para processar o comando de login
/// </summary>
public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, ApiResponse<LoginResponseDto>>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly JwtSettings _jwtSettings;
    private readonly ILogger<LoginCommandHandler> _logger;

    public LoginCommandHandler(
        IUsuarioRepository usuarioRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService,
        IOptions<JwtSettings> jwtSettings,
        ILogger<LoginCommandHandler> logger)
    {
        _usuarioRepository = usuarioRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
        _jwtSettings = jwtSettings.Value;
        _logger = logger;
    }

    public async Task<ApiResponse<LoginResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Tentativa de login para o username: {Username}", request.Username);

        // Buscar usuário por username
        var usuario = await _usuarioRepository.GetByUsernameAsync(request.Username, cancellationToken);

        if (usuario == null)
        {
            _logger.LogWarning("Usuário não encontrado: {Username}", request.Username);
            return ApiResponse<LoginResponseDto>.Fail("Username ou senha inválidos");
        }

        // Verificar senha
        if (!_passwordHasher.Verify(request.Password, usuario.PasswordHash))
        {
            _logger.LogWarning("Senha inválida para o usuário: {Username}", request.Username);
            return ApiResponse<LoginResponseDto>.Fail("Username ou senha inválidos");
        }

        // Verificar se o usuário está ativo
        if (usuario.Status != Shared.Enums.StatusUsuario.Ativo)
        {
            _logger.LogWarning("Usuário inativo: {Username}", request.Username);
            return ApiResponse<LoginResponseDto>.Fail("Usuário inativo");
        }

        // Gerar tokens
        var accessToken = _jwtTokenService.GenerateAccessToken(usuario);
        var refreshTokenValue = _jwtTokenService.GenerateRefreshToken();

        // Salvar refresh token no banco
        var refreshToken = new Core.Domain.Entities.RefreshToken
        {
            Token = refreshTokenValue,
            UsuarioId = usuario.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays),
            IsRevoked = false,
            IpAddress = null, // TODO: Capturar do HttpContext
            UserAgent = null  // TODO: Capturar do HttpContext
        };
        await _refreshTokenRepository.AddAsync(refreshToken, cancellationToken);

        // Atualizar último login
        usuario.UltimoLogin = DateTime.UtcNow;
        await _usuarioRepository.UpdateAsync(usuario, cancellationToken);

        var userInfo = new UserInfoDto
        {
            Id = usuario.Id.ToString(),
            Username = usuario.Username,
            Email = usuario.Email,
            Name = usuario.Nome,
            Roles = new List<string> { usuario.Tipo.ToString() }
        };

        var response = new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshTokenValue,
            ExpiresIn = _jwtSettings.ExpirationMinutes * 60,
            User = userInfo
        };

        _logger.LogInformation("Login bem-sucedido para: {Username}", request.Username);
        return ApiResponse<LoginResponseDto>.Ok(response, "Login realizado com sucesso");
    }
}

