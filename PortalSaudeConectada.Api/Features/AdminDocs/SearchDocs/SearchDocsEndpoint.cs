using Carter;
using FluentValidation;
using MediatR;
using PortalSaudeConectada.Shared.Contracts;
using SearchDocsRequestDto = PortalSaudeConectada.Shared.Dtos.AdminDocs.SearchDocsRequest;
using SearchDocsResponseDto = PortalSaudeConectada.Shared.Dtos.AdminDocs.SearchDocsResponse;

namespace PortalSaudeConectada.Api.Features.AdminDocs.SearchDocs;

public sealed class SearchDocsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/admin/docs/search", HandleAsync)
            .WithName("SearchDocs")
            .WithTags("📚 Admin - Documentação")
            .WithSummary("Buscar documentos")
            .WithDescription("Realiza busca full-text nos documentos")
            .Accepts<SearchDocsRequestDto>("application/json")
            .Produces<ApiResponse<SearchDocsResponseDto>>(200)
            .Produces<ApiResponse<SearchDocsResponseDto>>(400)
            .Produces(401)
            .RequireAuthorization("GestorOrAdmin");
    }

    private static async Task<IResult> HandleAsync(
        SearchDocsRequestDto request,
        IMediator mediator,
        IValidator<SearchDocsCommand> validator,
        CancellationToken cancellationToken)
    {
        var command = new SearchDocsCommand(request.Query, request.Projeto, request.Limit);

        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(e => new ValidationError(e.PropertyName, e.ErrorMessage))
                .ToList();

            var failResponse = ApiResponse<SearchDocsResponseDto>.ValidationFail(errors);
            return Results.BadRequest(failResponse);
        }

        var response = await mediator.Send(command, cancellationToken);

        return response.Success 
            ? Results.Ok(response) 
            : Results.BadRequest(response);
    }
}

