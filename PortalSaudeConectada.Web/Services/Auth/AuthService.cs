using System.Net.Http.Json;
using System.Text.Json;
using PortalSaudeConectada.Shared.Contracts;
using PortalSaudeConectada.Shared.Dtos.Auth;

namespace PortalSaudeConectada.Web.Services.Auth;

/// <summary>
/// Implementação do serviço de autenticação
/// </summary>
public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly ITokenStorageService _tokenStorage;
    private readonly ILogger<AuthService> _logger;
    private UserInfo? _currentUser;

    public AuthService(
        HttpClient httpClient,
        ITokenStorageService tokenStorage,
        ILogger<AuthService> logger)
    {
        _httpClient = httpClient;
        _tokenStorage = tokenStorage;
        _logger = logger;
    }

    public async Task<LoginResponse?> LoginAsync(string username, string password)
    {
        try
        {
            var request = new LoginRequest
            {
                Username = username,
                Password = password
            };

            var response = await _httpClient.PostAsJsonAsync("/api/auth/login", request);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Login falhou com status: {StatusCode}", response.StatusCode);
                return null;
            }

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<LoginResponse>>();

            if (apiResponse?.Success == true && apiResponse.Data != null)
            {
                // Armazenar tokens
                await _tokenStorage.SetAccessTokenAsync(apiResponse.Data.AccessToken);
                await _tokenStorage.SetRefreshTokenAsync(apiResponse.Data.RefreshToken);

                // Armazenar informações do usuário
                _currentUser = apiResponse.Data.User;

                _logger.LogInformation("Login bem-sucedido para: {Username}", username);
                return apiResponse.Data;
            }

            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao fazer login");
            return null;
        }
    }

    public async Task LogoutAsync()
    {
        try
        {
            // Limpar tokens locais
            await _tokenStorage.ClearTokensAsync();
            _currentUser = null;

            // Opcional: Chamar endpoint de logout na API
            // await _httpClient.PostAsync("/api/auth/logout", null);

            _logger.LogInformation("Logout realizado");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao fazer logout");
        }
    }

    public async Task<RefreshResponse?> RefreshTokenAsync()
    {
        try
        {
            var refreshToken = await _tokenStorage.GetRefreshTokenAsync();

            if (string.IsNullOrEmpty(refreshToken))
            {
                _logger.LogWarning("Refresh token não encontrado");
                return null;
            }

            var request = new RefreshRequest
            {
                RefreshToken = refreshToken
            };

            var response = await _httpClient.PostAsJsonAsync("/api/auth/refresh", request);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Refresh token falhou com status: {StatusCode}", response.StatusCode);
                await LogoutAsync(); // Token inválido, fazer logout
                return null;
            }

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<RefreshResponse>>();

            if (apiResponse?.Success == true && apiResponse.Data != null)
            {
                // Atualizar access token
                await _tokenStorage.SetAccessTokenAsync(apiResponse.Data.AccessToken);

                _logger.LogInformation("Token renovado com sucesso");
                return apiResponse.Data;
            }

            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao renovar token");
            return null;
        }
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var token = await _tokenStorage.GetAccessTokenAsync();
        return !string.IsNullOrEmpty(token);
    }

    public async Task<UserInfo?> GetCurrentUserAsync()
    {
        if (_currentUser != null)
        {
            return _currentUser;
        }

        // TODO: Decodificar JWT token para obter informações do usuário
        // Por enquanto, retorna null se não tiver em cache
        return null;
    }
}

