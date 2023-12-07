namespace projeto.infra.AsyncComm.Extensions;

public class Base
{
    /* Dados utilizados pelo publicador*/
    public string exchange = "projeto.adicionado/api.projetos";
    public string filaNome = "atualizar.estoque";
    public string routingKey = "projeto.atualizar.estoque";

    /* Dados utilizados pelo consumidor*/
    public string exchangeConsumer = "produtos/api.estoque";
    public string filaConsumerDisponiveis = "produtos.disponiveis";
    public string routingKeyConsumerAdicionado = "produtos.disponiveis.produto.adicionado";
    public string filaConsumerAtualizados = "produtos.disponiveis.atualizados";
    public string routingKeyConsumerAtualizado = "produtos.disponiveis.produto.atualizado";
    public string filaConsumerDeletados = "produtos.disponiveis.deletados";
    public string routingKeyConsumerDeletado = "produtos.disponiveis.produto.deletado";


}
