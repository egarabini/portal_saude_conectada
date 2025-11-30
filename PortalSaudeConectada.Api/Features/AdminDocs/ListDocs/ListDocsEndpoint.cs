using Carter;
using MediatR;
using PortalSaudeConectada.Shared.Contracts;
using DocsListResponseDto = PortalSaudeConectada.Shared.Dtos.AdminDocs.DocsListResponse;

namespace PortalSaudeConectada.Api.Features.AdminDocs.ListDocs;

public sealed class ListDocsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/admin/docs", HandleAsync)
            .WithName("ListDocs")
            .WithTags("📚 Admin - Documentação")
            .WithSummary("Listar documentos")
            .WithDescription("Lista todos os documentos disponíveis, opcionalmente filtrados por projeto")
            .Produces<ApiResponse<DocsListResponseDto>>(200)
            .Produces(401)
            .RequireAuthorization("GestorOrAdmin");
    }

    private static async Task<IResult> HandleAsync(
        string? projeto,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new ListDocsQuery(projeto);
        var response = await mediator.Send(query, cancellationToken);

        return response.Success 
            ? Results.Ok(response) 
            : Results.BadRequest(response);
    }
}

