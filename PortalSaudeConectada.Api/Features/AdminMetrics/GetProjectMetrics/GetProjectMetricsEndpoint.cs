using Carter;
using FluentValidation;
using MediatR;
using PortalSaudeConectada.Shared.Contracts;
using ProjectMetricsResponseDto = PortalSaudeConectada.Shared.Dtos.AdminMetrics.ProjectMetricsResponse;

namespace PortalSaudeConectada.Api.Features.AdminMetrics.GetProjectMetrics;

/// <summary>
/// Endpoint para obter métricas de um projeto específico
/// </summary>
public sealed class GetProjectMetricsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/admin/metrics/{projeto}", HandleAsync)
            .WithName("GetProjectMetrics")
            .WithTags("📊 Admin - Métricas")
            .WithSummary("Métricas de um projeto específico")
            .WithDescription("Retorna métricas detalhadas de um projeto específico")
            .Produces<ApiResponse<ProjectMetricsResponseDto>>(200)
            .Produces<ApiResponse<ProjectMetricsResponseDto>>(400)
            .Produces<ApiResponse<ProjectMetricsResponseDto>>(404)
            .Produces(401)
            .RequireAuthorization("GestorOrAdmin");
    }

    private static async Task<IResult> HandleAsync(
        string projeto,
        IMediator mediator,
        IValidator<GetProjectMetricsQuery> validator,
        CancellationToken cancellationToken)
    {
        var query = new GetProjectMetricsQuery(projeto);

        var validationResult = await validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(e => new ValidationError(e.PropertyName, e.ErrorMessage))
                .ToList();

            var failResponse = ApiResponse<ProjectMetricsResponseDto>.ValidationFail(errors);
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

