using PortalSaudeConectada.Core.Domain.Exceptions;

namespace PortalSaudeConectada.Core.Domain.ValueObjects;

/// <summary>
/// Value Object para Email com validação embutida
/// </summary>
public class Email : ValueObject
{
    public string Endereco { get; }

    private Email(string endereco) => Endereco = endereco.ToLowerInvariant();

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("Email é obrigatório");

        if (!IsValid(email))
            throw new DomainException("Email inválido");

        return new Email(email);
    }

    public static bool IsValid(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Endereco;
    }

    public override string ToString() => Endereco;

    public static implicit operator string(Email email) => email.Endereco;
}

