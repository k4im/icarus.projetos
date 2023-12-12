namespace projeto.domain.ValueObject;

public class Telefone
{
    public Telefone(
        sbyte dDD, 
        char digitoNove, 
        string numero)
    {
        DDD = ValidarDDD(dDD);
        DigitoNove = ValidarNumeroNove(digitoNove);
        Numero = ValidarTelefone(numero);
    }

    public sbyte DDD { get; set; }
    public char DigitoNove { get; set; }
    public string Numero {get; set; }

    string ValidarTelefone(string telefone) {
        var numeroLimpo = telefone.Trim().Replace("-", "");
        if (!Regex.IsMatch(numeroLimpo, @"^[0-9 ]+$", RegexOptions.Compiled))
            throw new Exception("O numero de telefone precisa conter apenas numeros");
        return numeroLimpo;
    }  
    char ValidarNumeroNove(char numeroNove) {
        if (numeroNove == '0') throw new Exception("O valor do digito nove não pode ser zero");
        return numeroNove;
    }

    sbyte ValidarDDD(sbyte ddd) {
        if(ddd < 0) throw new Exception("O ddd não pode ter o valor de zero");
        if(ddd < -1) throw new Exception("O ddd não pode ter um valor negativo");
        return ddd;
    }
}
