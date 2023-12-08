using RabbitMQ.Client;

namespace projeto.servicebusAdapter;
public abstract class Base
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


    public void CriarFilas(IModel channel)
    {
        // Definindo a fila no RabbitMQ
        channel.QueueDeclare(queue: filaNome, durable: true,
            exclusive: false,
            autoDelete: false);

        // Definindo o Exchange no RabbitMQ
        channel.ExchangeDeclare(exchange: exchange,
        type: ExchangeType.Topic,
        durable: true,
        autoDelete: false);

        // Linkando a fila ao exchange
        channel.QueueBind(queue: filaNome,
            exchange: exchange,
            routingKey: routingKey);


    }
}
