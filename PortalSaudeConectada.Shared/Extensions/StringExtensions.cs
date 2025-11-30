namespace PortalSaudeConectada.Shared.Extensions;

/// <summary>
/// Extensões para strings
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Remove caracteres não numéricos de uma string
    /// </summary>
    public static string OnlyNumbers(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        return new string(value.Where(char.IsDigit).ToArray());
    }

    /// <summary>
    /// Verifica se a string é nula ou vazia
    /// </summary>
    public static bool IsNullOrEmpty(this string? value)
    {
        return string.IsNullOrEmpty(value);
    }

    /// <summary>
    /// Verifica se a string é nula, vazia ou contém apenas espaços
    /// </summary>
    public static bool IsNullOrWhiteSpace(this string? value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    /// <summary>
    /// Trunca a string para o tamanho máximo especificado
    /// </summary>
    public static string Truncate(this string value, int maxLength, string suffix = "...")
    {
        if (string.IsNullOrEmpty(value) || value.Length <= maxLength)
            return value;

        return value.Substring(0, maxLength - suffix.Length) + suffix;
    }

    /// <summary>
    /// Converte para Title Case (Primeira Letra Maiúscula)
    /// </summary>
    public static string ToTitleCase(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
    }
}

