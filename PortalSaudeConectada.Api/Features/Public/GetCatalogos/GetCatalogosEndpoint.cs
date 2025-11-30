using Carter;
using MediatR;
using PortalSaudeConectada.Shared.Contracts;
using CatalogosResponseDto = PortalSaudeConectada.Shared.Dtos.Public.CatalogosResponse;

namespace PortalSaudeConectada.Api.Features.Public.GetCatalogos;

public sealed class GetCatalogosEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/public/catalogos", HandleAsync)
            .WithName("GetCatalogos")
            .WithTags("🌐 Público")
            .WithSummary("Catálogo de projetos e features")
            .WithDescription("Lista todos os projetos e features disponíveis no Portal Saúde Conectada")
            .Produces<ApiResponse<CatalogosResponseDto>>(200)
            .AllowAnonymous();
    }

    private static async Task<IResult> HandleAsync(
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetCatalogosQuery();
        var response = await mediator.Send(query, cancellationToken);

        return response.Success 
            ? Results.Ok(response) 
            : Results.BadRequest(response);
    }
}

