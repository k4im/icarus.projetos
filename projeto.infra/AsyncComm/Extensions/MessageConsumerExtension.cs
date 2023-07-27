
namespace projeto.infra.AsyncComm.Extensions
{
    public class MessageConsumerExtension : Base
    {
        public void criarFilas(IModel channel)
        {

            // Declarando a fila para eventos que foram adicionados
            channel.QueueDeclare(queue: "produtos.disponiveis",
                durable: true,
                exclusive: false,
                autoDelete: false);


            // Declarando a fila para eventos que foram deletados
            channel.QueueDeclare(queue: "produtos.disponiveis.deletados",
                durable: true,
                exclusive: false,
                autoDelete: false);

            // Declarando a fila para eventos que foram atualizados
            channel.QueueDeclare(queue: "produtos.disponiveis.atualizados",
                durable: true,
                exclusive: false,
                autoDelete: false);

            // Definindo o balanceamento da fila para uma mensagem por vez.
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            // Linkando a fila de eventos adicionados ao exchange 
            channel.QueueBind(queue: "produtos.disponiveis",
                exchange: "produtos/api.estoque",
                routingKey: "produtos.disponiveis.produto.adicionado");

            // Linkando a fila de eventos deletados ao exchange 
            channel.QueueBind(queue: "produtos.disponiveis.deletados",
                exchange: "produtos/api.estoque",
                routingKey: "produtos.disponiveis.produto.deletado");

            // Linkando a fila de eventos atualizados ao exchange 
            channel.QueueBind(queue: "produtos.disponiveis.atualizados",
                exchange: "produtos/api.estoque",
                routingKey: "produtos.disponiveis.produto.atualizado");



        }
    }
}