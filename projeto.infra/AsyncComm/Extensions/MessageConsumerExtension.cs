
namespace projeto.infra.AsyncComm.Extensions
{
    public class MessageConsumerExtension : Base
    {
        public void criarFilas(IModel channel)
        {

            // Declarando a fila para eventos que foram adicionados
            channel.QueueDeclare(queue: filaConsumerDisponiveis,
                durable: true,
                exclusive: false,
                autoDelete: false);


            // Declarando a fila para eventos que foram deletados
            channel.QueueDeclare(queue: filaConsumerDeletados,
                durable: true,
                exclusive: false,
                autoDelete: false);

            // Declarando a fila para eventos que foram atualizados
            channel.QueueDeclare(queue: filaConsumerAtualizados,
                durable: true,
                exclusive: false,
                autoDelete: false);

            // Definindo o balanceamento da fila para uma mensagem por vez.
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            // Linkando a fila de eventos adicionados ao exchange 
            channel.QueueBind(queue: filaConsumerDisponiveis,
                exchange: exchangeConsumer,
                routingKey: routingKeyConsumerAdicionado);

            // Linkando a fila de eventos deletados ao exchange 
            channel.QueueBind(queue: filaConsumerDeletados,
                exchange: exchangeConsumer,
                routingKey: routingKeyConsumerDeletado);

            // Linkando a fila de eventos atualizados ao exchange 
            channel.QueueBind(queue: filaConsumerAtualizados,
                exchange: exchangeConsumer,
                routingKey: routingKeyConsumerAtualizado);



        }
    }
}