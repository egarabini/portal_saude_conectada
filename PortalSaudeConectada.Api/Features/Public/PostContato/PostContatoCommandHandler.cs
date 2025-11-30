using PortalSaudeConectada.Core.Application.Commands;
using PortalSaudeConectada.Shared.Contracts;
using ContatoResponseDto = PortalSaudeConectada.Shared.Dtos.Public.ContatoResponse;

namespace PortalSaudeConectada.Api.Features.Public.PostContato;

public sealed class PostContatoCommandHandler : ICommandHandler<PostContatoCommand, ApiResponse<ContatoResponseDto>>
{
    private readonly ILogger<PostContatoCommandHandler> _logger;

    public PostContatoCommandHandler(ILogger<PostContatoCommandHandler> logger)
    {
        _logger = logger;
    }

    public async Task<ApiResponse<ContatoResponseDto>> Handle(PostContatoCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processando contato de: {Nome} ({Email})", request.Nome, request.Email);

        // TODO: Validar e persistir solicitação de contato
        // TODO: Enviar notificação por email/webhook
        await Task.CompletedTask; // Simula operação assíncrona

        var protocolo = Guid.NewGuid().ToString("N")[..8].ToUpperInvariant();

        var response = new ContatoResponseDto
        {
            Success = true,
            Message = "Solicitação recebida com sucesso. Entraremos em contato em breve.",
            Protocolo = protocolo
        };

        _logger.LogInformation("Contato processado. Protocolo: {Protocolo}", protocolo);
        return ApiResponse<ContatoResponseDto>.Ok(response);
    }
}

