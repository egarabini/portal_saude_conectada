using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace PortalSaudeConectada.Web.Services.Auth;

/// <summary>
/// Implementação do serviço de armazenamento de tokens usando ProtectedSessionStorage
/// </summary>
public class TokenStorageService : ITokenStorageService
{
    private readonly ProtectedSessionStorage _sessionStorage;
    private const string AccessTokenKey = "access_token";
    private const string RefreshTokenKey = "refresh_token";

    public TokenStorageService(ProtectedSessionStorage sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }

    public async Task SetAccessTokenAsync(string token)
    {
        await _sessionStorage.SetAsync(AccessTokenKey, token);
    }

    public async Task<string?> GetAccessTokenAsync()
    {
        try
        {
            var result = await _sessionStorage.GetAsync<string>(AccessTokenKey);
            return result.Success ? result.Value : null;
        }
        catch
        {
            return null;
        }
    }

    public async Task SetRefreshTokenAsync(string token)
    {
        await _sessionStorage.SetAsync(RefreshTokenKey, token);
    }

    public async Task<string?> GetRefreshTokenAsync()
    {
        try
        {
            var result = await _sessionStorage.GetAsync<string>(RefreshTokenKey);
            return result.Success ? result.Value : null;
        }
        catch
        {
            return null;
        }
    }

    public async Task ClearTokensAsync()
    {
        await _sessionStorage.DeleteAsync(AccessTokenKey);
        await _sessionStorage.DeleteAsync(RefreshTokenKey);
    }
}

