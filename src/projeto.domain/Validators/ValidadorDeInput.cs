namespace projeto.domain.Validators;
public static class ValidadorDeInput
{
    public static bool ValidarInputNulo(string valor) {
        if(string.IsNullOrEmpty(valor)) return true;
        return false;
    }

    public static bool ValidarInputRegex(string valor) {
        if (!Regex.IsMatch(valor, @"^[a-zA-Zà-úÀ-Ú0-9 ]+$", RegexOptions.Compiled)) return true;
        return false;
    }
    public static bool ValidarMenorDoQueZero(double valor) {
        if(valor < 0) return true;
        return false;
    }

    public static bool ValidarMenorDoQueZero(int valor) {
        if(valor < 0) return true;
        return false;
    }
}
