using FluentValidation;

namespace PortalSaudeConectada.Api.Features.Public.PostContato;

public sealed class PostContatoValidator : AbstractValidator<PostContatoCommand>
{
    public PostContatoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(200).WithMessage("Nome deve ter no máximo 200 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório")
            .EmailAddress().WithMessage("Email inválido")
            .MaximumLength(200).WithMessage("Email deve ter no máximo 200 caracteres");

        RuleFor(x => x.Telefone)
            .MaximumLength(50).WithMessage("Telefone deve ter no máximo 50 caracteres")
            .When(x => !string.IsNullOrEmpty(x.Telefone));

        RuleFor(x => x.Organizacao)
            .MaximumLength(200).WithMessage("Organização deve ter no máximo 200 caracteres")
            .When(x => !string.IsNullOrEmpty(x.Organizacao));

        RuleFor(x => x.Mensagem)
            .NotEmpty().WithMessage("Mensagem é obrigatória")
            .MinimumLength(10).WithMessage("Mensagem deve ter no mínimo 10 caracteres")
            .MaximumLength(2000).WithMessage("Mensagem deve ter no máximo 2000 caracteres");
    }
}

