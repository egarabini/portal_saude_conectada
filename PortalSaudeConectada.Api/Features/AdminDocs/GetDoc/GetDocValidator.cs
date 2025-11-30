using FluentValidation;

namespace PortalSaudeConectada.Api.Features.AdminDocs.GetDoc;

public sealed class GetDocValidator : AbstractValidator<GetDocQuery>
{
    public GetDocValidator()
    {
        RuleFor(x => x.Projeto)
            .NotEmpty().WithMessage("Projeto é obrigatório")
            .MaximumLength(100).WithMessage("Projeto deve ter no máximo 100 caracteres");

        RuleFor(x => x.Filepath)
            .NotEmpty().WithMessage("Filepath é obrigatório")
            .MaximumLength(500).WithMessage("Filepath deve ter no máximo 500 caracteres");
    }
}

