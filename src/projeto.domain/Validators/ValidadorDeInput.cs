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

    public static bool ValidarTamanhoDoCPF (string valor) {
        if (!Regex.IsMatch(valor, @"^[0-9 ]+$", RegexOptions.Compiled)) return true;
        if (valor.Length > 11 || valor.Length < 11) return true;
        return false;
    }

    public static bool ValidarDescricao(string desc) {
        if(desc.Length >= 80) throw new Exception(
            @"O campo de descrição não pode conter mais do que 80 caracteres");
        if (!Regex.IsMatch(desc, @"^[a-zA-Zà-úÀ-Ú0-9 ]+$", RegexOptions.Compiled)) return true;
        return false;
    }
}
