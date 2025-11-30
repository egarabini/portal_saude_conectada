using Carter;
using FluentValidation;
using MediatR;
using PortalSaudeConectada.Shared.Contracts;
using LoginRequestDto = PortalSaudeConectada.Shared.Dtos.Auth.LoginRequest;
using LoginResponseDto = PortalSaudeConectada.Shared.Dtos.Auth.LoginResponse;

namespace PortalSaudeConectada.Api.Features.Auth.Login;

/// <summary>
/// Endpoint para login usando Carter
/// </summary>
public sealed class LoginEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/login", HandleAsync)
            .WithName("Login")
            .WithTags("🔐 Autenticação")
            .WithSummary("Fazer login")
            .WithDescription("Autentica um usuário e retorna tokens de acesso")
            .Accepts<LoginRequestDto>("application/json")
            .Produces<ApiResponse<LoginResponseDto>>(200)
            .Produces<ApiResponse<LoginResponseDto>>(400)
            .Produces<ApiResponse<LoginResponseDto>>(401)
            .AllowAnonymous();
    }

    private static async Task<IResult> HandleAsync(
        LoginRequestDto request,
        IMediator mediator,
        IValidator<LoginCommand> validator,
        CancellationToken cancellationToken)
    {
        // Criar o command
        var command = new LoginCommand(request.Username, request.Password);

        // Validar o command
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(e => new ValidationError(e.PropertyName, e.ErrorMessage))
                .ToList();

            var failResponse = ApiResponse<LoginResponseDto>.ValidationFail(errors);
            return Results.BadRequest(failResponse);
        }

        // Executar o command via MediatR
        var response = await mediator.Send(command, cancellationToken);

        // Retornar resultado
        if (response.Success)
        {
            return Results.Ok(response);
        }

        return Results.Unauthorized();
    }
}

