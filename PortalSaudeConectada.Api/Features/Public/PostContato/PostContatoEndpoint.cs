using Carter;
using FluentValidation;
using MediatR;
using PortalSaudeConectada.Shared.Contracts;
using ContatoRequestDto = PortalSaudeConectada.Shared.Dtos.Public.ContatoRequest;
using ContatoResponseDto = PortalSaudeConectada.Shared.Dtos.Public.ContatoResponse;

namespace PortalSaudeConectada.Api.Features.Public.PostContato;

public sealed class PostContatoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/public/contato", HandleAsync)
            .WithName("PostContato")
            .WithTags("🌐 Público")
            .WithSummary("Enviar mensagem de contato")
            .WithDescription("Envia uma solicitação de contato que será processada pela equipe")
            .Accepts<ContatoRequestDto>("application/json")
            .Produces<ApiResponse<ContatoResponseDto>>(200)
            .Produces<ApiResponse<ContatoResponseDto>>(400)
            .AllowAnonymous();
    }

    private static async Task<IResult> HandleAsync(
        ContatoRequestDto request,
        IMediator mediator,
        IValidator<PostContatoCommand> validator,
        CancellationToken cancellationToken)
    {
        var command = new PostContatoCommand(
            request.Nome,
            request.Email,
            request.Telefone,
            request.Organizacao,
            request.Mensagem
        );

        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(e => new ValidationError(e.PropertyName, e.ErrorMessage))
                .ToList();

            var failResponse = ApiResponse<ContatoResponseDto>.ValidationFail(errors);
            return Results.BadRequest(failResponse);
        }

        var response = await mediator.Send(command, cancellationToken);

        return response.Success 
            ? Results.Ok(response) 
            : Results.BadRequest(response);
    }
}

