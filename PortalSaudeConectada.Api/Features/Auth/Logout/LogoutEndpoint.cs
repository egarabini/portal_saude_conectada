using Carter;
using FluentValidation;
using MediatR;
using PortalSaudeConectada.Shared.Contracts;
using LogoutRequestDto = PortalSaudeConectada.Shared.Dtos.Auth.LogoutRequest;
using LogoutResponseDto = PortalSaudeConectada.Shared.Dtos.Auth.LogoutResponse;

namespace PortalSaudeConectada.Api.Features.Auth.Logout;

/// <summary>
/// Endpoint para realizar logout
/// </summary>
public sealed class LogoutEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/logout", HandleAsync)
            .WithName("Logout")
            .WithTags("🔐 Autenticação")
            .WithSummary("Fazer logout")
            .WithDescription("Invalida o refresh token e realiza logout do usuário")
            .Accepts<LogoutRequestDto>("application/json")
            .Produces<ApiResponse<LogoutResponseDto>>(200)
            .Produces<ApiResponse<LogoutResponseDto>>(400)
            .RequireAuthorization(); // Logout requer autenticação
    }

    private static async Task<IResult> HandleAsync(
        LogoutRequestDto request,
        IMediator mediator,
        IValidator<LogoutCommand> validator,
        CancellationToken cancellationToken)
    {
        var command = new LogoutCommand(request.RefreshToken);

        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(e => new ValidationError(e.PropertyName, e.ErrorMessage))
                .ToList();

            var failResponse = ApiResponse<LogoutResponseDto>.ValidationFail(errors);
            return Results.BadRequest(failResponse);
        }

        var response = await mediator.Send(command, cancellationToken);

        return response.Success 
            ? Results.Ok(response) 
            : Results.BadRequest(response);
    }
}

