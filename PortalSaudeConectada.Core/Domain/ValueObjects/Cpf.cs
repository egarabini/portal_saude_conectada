using PortalSaudeConectada.Core.Domain.Exceptions;

namespace PortalSaudeConectada.Core.Domain.ValueObjects;

/// <summary>
/// Value Object para CPF com validação embutida
/// </summary>
public class Cpf : ValueObject
{
    public string Numero { get; }

    private Cpf(string numero) => Numero = numero;

    public static Cpf Create(string cpf)
    {
        var apenasNumeros = new string(cpf.Where(char.IsDigit).ToArray());

        if (!IsValid(apenasNumeros))
            throw new DomainException("CPF inválido");

        return new Cpf(apenasNumeros);
    }

    public static bool IsValid(string cpf)
    {
        if (string.IsNullOrEmpty(cpf) || cpf.Length != 11)
            return false;

        if (cpf.Distinct().Count() == 1)
            return false;

        var soma1 = 0;
        for (int i = 0; i < 9; i++)
            soma1 += int.Parse(cpf[i].ToString()) * (10 - i);
        var resto1 = soma1 % 11;
        var digito1 = resto1 < 2 ? 0 : 11 - resto1;

        var soma2 = 0;
        for (int i = 0; i < 10; i++)
            soma2 += int.Parse(cpf[i].ToString()) * (11 - i);
        var resto2 = soma2 % 11;
        var digito2 = resto2 < 2 ? 0 : 11 - resto2;

        return cpf.EndsWith($"{digito1}{digito2}");
    }

    public string Formatado => $"{Numero[..3]}.{Numero[3..6]}.{Numero[6..9]}-{Numero[9..]}";

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Numero;
    }

    public override string ToString() => Numero;

    public static implicit operator string(Cpf cpf) => cpf.Numero;
}

