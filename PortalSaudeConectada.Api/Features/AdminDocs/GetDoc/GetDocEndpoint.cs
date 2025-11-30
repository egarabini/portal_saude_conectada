using Carter;
using FluentValidation;
using MediatR;
using PortalSaudeConectada.Shared.Contracts;
using DocDetailResponseDto = PortalSaudeConectada.Shared.Dtos.AdminDocs.DocDetailResponse;

namespace PortalSaudeConectada.Api.Features.AdminDocs.GetDoc;

public sealed class GetDocEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/admin/docs/{projeto}/{*filepath}", HandleAsync)
            .WithName("GetDoc")
            .WithTags("📚 Admin - Documentação")
            .WithSummary("Obter documento específico")
            .WithDescription("Retorna o conteúdo de um documento específico com histórico de versões")
            .Produces<ApiResponse<DocDetailResponseDto>>(200)
            .Produces<ApiResponse<DocDetailResponseDto>>(400)
            .Produces<ApiResponse<DocDetailResponseDto>>(404)
            .Produces(401)
            .RequireAuthorization("GestorOrAdmin");
    }

    private static async Task<IResult> HandleAsync(
        string projeto,
        string filepath,
        IMediator mediator,
        IValidator<GetDocQuery> validator,
        CancellationToken cancellationToken)
    {
        var query = new GetDocQuery(projeto, filepath);

        var validationResult = await validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(e => new ValidationError(e.PropertyName, e.ErrorMessage))
                .ToList();

            var failResponse = ApiResponse<DocDetailResponseDto>.ValidationFail(errors);
            return Results.BadRequest(failResponse);
        }

        var response = await mediator.Send(query, cancellationToken);

        if (response.Success)
        {
            return Results.Ok(response);
        }

        return Results.NotFound(response);
    }
}

