using FluentValidation;

namespace PortalSaudeConectada.Api.Features.AdminDocs.SearchDocs;

public sealed class SearchDocsValidator : AbstractValidator<SearchDocsCommand>
{
    public SearchDocsValidator()
    {
        RuleFor(x => x.Query)
            .NotEmpty().WithMessage("Query é obrigatória")
            .MinimumLength(2).WithMessage("Query deve ter no mínimo 2 caracteres")
            .MaximumLength(200).WithMessage("Query deve ter no máximo 200 caracteres");

        RuleFor(x => x.Limit)
            .GreaterThan(0).WithMessage("Limit deve ser maior que 0")
            .LessThanOrEqualTo(100).WithMessage("Limit deve ser no máximo 100");
    }
}

