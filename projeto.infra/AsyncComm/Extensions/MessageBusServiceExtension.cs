
namespace projeto.infra.AsyncComm.Extensions
{
    public class MessageBusServiceExtension : Base
    {
        public void criarFilas(IModel channel)
        {
            // Definindo a fila no RabbitMQ
            channel.QueueDeclare(queue: "atualizar.estoque", durable: true,
                exclusive: false,
                autoDelete: false);

            // Definindo o Exchange no RabbitMQ
            channel.ExchangeDeclare(exchange: "projeto.adicionado/api.projetos",
            type: ExchangeType.Topic,
            durable: true,
            autoDelete: false);

            // Linkando a fila ao exchange
            channel.QueueBind(queue: "atualizar.estoque",
                exchange: "projeto.adicionado/api.projetos",
                routingKey: "projeto.atualizar.estoque");


        }
    }
}