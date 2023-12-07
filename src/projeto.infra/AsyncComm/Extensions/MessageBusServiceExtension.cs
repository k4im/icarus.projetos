
namespace projeto.infra.AsyncComm.Extensions;

public abstract class MessageBusServiceExtension : Base
{
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
