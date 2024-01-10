namespace projeto.domain.ValueObject;

public class Telefone
{
    public Telefone(
        sbyte dDD, 
        string dDI, 
        string numero, 
        string ramal, 
        bool telefonePrincipal, 
        Guid tipoTelefoneId)
    {
        DDD = ValidarDDD(dDD);
        DDI = ValidarDDI(dDI);
        Numero = ValidarTelefone(numero);
        Ramal = ramal;
        TelefonePrincipal = telefonePrincipal;
        TipoTelefoneId = tipoTelefoneId;
    }

    [Key]
    public Guid Id { get; set; }
    [DataType("VARCHAR(2)")]
    public sbyte DDD { get; set; }
    [DataType("VARCHAR(999)")]
    public string DDI { get; set; }
    [DataType("VARCHAR(15)")]
    public string Numero {get; set; }
    [DataType("VARCHAR")]
    public string Ramal { get; set; }
    public bool TelefonePrincipal { get; set; }
    [DataType("VARCHAR")]
    public string Observacoes { get; set; }
    
    // Chaves estrangeiras 
    public Guid TipoTelefoneId { get; set; }
    public Guid PessoaId { get; set; }



    string ValidarTelefone(string telefone) {
        var numeroLimpo = telefone.Trim().Replace("-", "");
        if (!Regex.IsMatch(numeroLimpo, @"^[0-9 ]+$", RegexOptions.Compiled))
            throw new Exception("O numero de telefone precisa conter apenas numeros");
        return numeroLimpo;
    }  
    string ValidarDDI(string ddi) {
        if (ddi == "0") throw new Exception("O valor do DDI não pode ser zero");
        return ddi;
    }

    sbyte ValidarDDD(sbyte ddd) {
        if(ddd < 0) throw new Exception("O ddd não pode ter o valor de zero");
        if(ddd < -1) throw new Exception("O ddd não pode ter um valor negativo");
        return ddd;
    }
}
