using FluentValidation;

namespace PortalSaudeConectada.Api.Features.Auth.RefreshToken;

/// <summary>
/// Validador para RefreshTokenCommand
/// </summary>
public sealed class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Refresh token é obrigatório")
            .MinimumLength(10).WithMessage("Refresh token inválido");
    }
}

