using System.Net;
using System.Net.Http.Headers;

namespace PortalSaudeConectada.Web.Services.Auth;

/// <summary>
/// Handler que adiciona o JWT token automaticamente em todas as requisições HTTP
/// e renova o token automaticamente quando expirado
/// </summary>
public class AuthorizationMessageHandler : DelegatingHandler
{
    private readonly ITokenStorageService _tokenStorage;
    private readonly IAuthService _authService;
    private readonly ILogger<AuthorizationMessageHandler> _logger;
    private static readonly SemaphoreSlim _refreshSemaphore = new(1, 1);

    public AuthorizationMessageHandler(
        ITokenStorageService tokenStorage,
        IAuthService authService,
        ILogger<AuthorizationMessageHandler> logger)
    {
        _tokenStorage = tokenStorage;
        _authService = authService;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // Obter o access token
        var token = await _tokenStorage.GetAccessTokenAsync();

        if (!string.IsNullOrEmpty(token))
        {
            // Adicionar o token no header Authorization
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _logger.LogDebug("Token JWT adicionado à requisição: {Method} {Uri}", request.Method, request.RequestUri);
        }
        else
        {
            _logger.LogDebug("Nenhum token disponível para a requisição: {Method} {Uri}", request.Method, request.RequestUri);
        }

        var response = await base.SendAsync(request, cancellationToken);

        // Se receber 401 Unauthorized, tentar renovar o token
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            _logger.LogWarning("Recebido 401 Unauthorized. Tentando renovar token...");

            // Usar semáforo para evitar múltiplas renovações simultâneas
            await _refreshSemaphore.WaitAsync(cancellationToken);
            try
            {
                var refreshResult = await _authService.RefreshTokenAsync();

                if (refreshResult != null)
                {
                    _logger.LogInformation("Token renovado com sucesso. Reenviando requisição...");

                    // Clonar a requisição original e adicionar o novo token
                    var newRequest = await CloneHttpRequestMessageAsync(request);
                    newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", refreshResult.AccessToken);

                    // Reenviar a requisição com o novo token
                    response = await base.SendAsync(newRequest, cancellationToken);
                }
                else
                {
                    _logger.LogWarning("Falha ao renovar token. Usuário precisa fazer login novamente.");
                }
            }
            finally
            {
                _refreshSemaphore.Release();
            }
        }

        return response;
    }

    private static async Task<HttpRequestMessage> CloneHttpRequestMessageAsync(HttpRequestMessage request)
    {
        var clone = new HttpRequestMessage(request.Method, request.RequestUri)
        {
            Version = request.Version
        };

        if (request.Content != null)
        {
            var content = await request.Content.ReadAsStringAsync();
            clone.Content = new StringContent(content);

            foreach (var header in request.Content.Headers)
            {
                clone.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
        }

        foreach (var header in request.Headers)
        {
            clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        return clone;
    }
}

