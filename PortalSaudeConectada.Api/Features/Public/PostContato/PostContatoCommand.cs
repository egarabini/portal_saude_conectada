using PortalSaudeConectada.Core.Application.Commands;
using PortalSaudeConectada.Shared.Contracts;
using ContatoResponseDto = PortalSaudeConectada.Shared.Dtos.Public.ContatoResponse;

namespace PortalSaudeConectada.Api.Features.Public.PostContato;

public sealed record PostContatoCommand(
    string Nome,
    string Email,
    string? Telefone,
    string? Organizacao,
    string Mensagem
) : ICommand<ApiResponse<ContatoResponseDto>>;

