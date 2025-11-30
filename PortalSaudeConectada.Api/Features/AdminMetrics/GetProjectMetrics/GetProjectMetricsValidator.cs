using FluentValidation;

namespace PortalSaudeConectada.Api.Features.AdminMetrics.GetProjectMetrics;

/// <summary>
/// Validador para GetProjectMetricsQuery
/// </summary>
public sealed class GetProjectMetricsValidator : AbstractValidator<GetProjectMetricsQuery>
{
    public GetProjectMetricsValidator()
    {
        RuleFor(x => x.Projeto)
            .NotEmpty().WithMessage("Nome do projeto é obrigatório")
            .MaximumLength(100).WithMessage("Nome do projeto deve ter no máximo 100 caracteres");
    }
}

