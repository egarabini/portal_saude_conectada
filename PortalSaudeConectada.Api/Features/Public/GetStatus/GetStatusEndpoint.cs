using Carter;
using MediatR;
using PortalSaudeConectada.Shared.Contracts;
using StatusResponseDto = PortalSaudeConectada.Shared.Dtos.Public.StatusResponse;

namespace PortalSaudeConectada.Api.Features.Public.GetStatus;

public sealed class GetStatusEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/public/status", HandleAsync)
            .WithName("GetStatus")
            .WithTags("🌐 Público")
            .WithSummary("Status do sistema")
            .WithDescription("Retorna o status de saúde do sistema e dos serviços integrados")
            .Produces<ApiResponse<StatusResponseDto>>(200)
            .AllowAnonymous();
    }

    private static async Task<IResult> HandleAsync(
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetStatusQuery();
        var response = await mediator.Send(query, cancellationToken);

        return response.Success 
            ? Results.Ok(response) 
            : Results.BadRequest(response);
    }
}

