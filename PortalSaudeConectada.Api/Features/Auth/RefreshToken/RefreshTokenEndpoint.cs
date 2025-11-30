using Carter;
using FluentValidation;
using MediatR;
using PortalSaudeConectada.Shared.Contracts;
using RefreshRequestDto = PortalSaudeConectada.Shared.Dtos.Auth.RefreshRequest;
using RefreshResponseDto = PortalSaudeConectada.Shared.Dtos.Auth.RefreshResponse;

namespace PortalSaudeConectada.Api.Features.Auth.RefreshToken;

/// <summary>
/// Endpoint para renovar token de acesso
/// </summary>
public sealed class RefreshTokenEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/refresh", HandleAsync)
            .WithName("RefreshToken")
            .WithTags("🔐 Autenticação")
            .WithSummary("Renovar token de acesso")
            .WithDescription("Renova o token de acesso usando um refresh token válido")
            .Accepts<RefreshRequestDto>("application/json")
            .Produces<ApiResponse<RefreshResponseDto>>(200)
            .Produces<ApiResponse<RefreshResponseDto>>(400)
            .Produces<ApiResponse<RefreshResponseDto>>(401)
            .AllowAnonymous();
    }

    private static async Task<IResult> HandleAsync(
        RefreshRequestDto request,
        IMediator mediator,
        IValidator<RefreshTokenCommand> validator,
        CancellationToken cancellationToken)
    {
        var command = new RefreshTokenCommand(request.RefreshToken);

        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(e => new ValidationError(e.PropertyName, e.ErrorMessage))
                .ToList();

            var failResponse = ApiResponse<RefreshResponseDto>.ValidationFail(errors);
            return Results.BadRequest(failResponse);
        }

        var response = await mediator.Send(command, cancellationToken);

        if (response.Success)
        {
            return Results.Ok(response);
        }

        return Results.Unauthorized();
    }
}

