using FluentValidation;

namespace PortalSaudeConectada.Api.Features.Auth.Logout;

/// <summary>
/// Validador para LogoutCommand
/// </summary>
public sealed class LogoutValidator : AbstractValidator<LogoutCommand>
{
    public LogoutValidator()
    {
        // Refresh token é opcional para logout
        // Se fornecido, deve ser válido
        RuleFor(x => x.RefreshToken)
            .MinimumLength(10).WithMessage("Refresh token inválido")
            .When(x => !string.IsNullOrEmpty(x.RefreshToken));
    }
}

