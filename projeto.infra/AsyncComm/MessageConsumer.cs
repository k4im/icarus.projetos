namespace projeto.infra.AsyncComm
{

    public class MessageConsumer : MessageConsumerExtension, IMessageConsumer
    {
        private readonly IConfiguration _config;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IRepoProdutosDisponiveis _repo;

        public MessageConsumer(IConfiguration config, IRepoProdutosDisponiveis repo)
        {
            _config = config;
            _repo = repo;
            var factory = new ConnectionFactory()
            {
                HostName = _config["RabbitMQ"],
                Port = int.Parse(_config["RabbitPort"]),
                UserName = _config["RabbitMQUser"],
                Password = _config["RabbitMQPwd"],
            };
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                criarFilas(_channel);
                _connection.ConnectionShutdown += RabbitMQFailed;
            }
            catch (Exception e)
            {
                Console.WriteLine($"--> Não foi possivel se conectar ao Message Bus: {e.Message}");
            }
        }

        public void verificarFila()
        {

            if (_channel.MessageCount(filaConsumerDisponiveis) != 0) consumirProdutosDisponiveis(_channel);
            if (_channel.MessageCount(filaConsumerAtualizados) != 0) consumirProdutosAtualizados(_channel);
            if (_channel.MessageCount(filaConsumerDeletados) != 0) consumirProdutosDeletados(_channel);
        }

        void consumirProdutosDisponiveis(IModel channel)
        {
            // Definindo um consumidor
            var consumer = new EventingBasicConsumer(channel);

            // seta o EventSlim
            // var msgsRecievedGate = new ManualResetEventSlim(false);

            // Definindo o que o consumidor recebe
            consumer.Received += async (model, ea) =>
            {
                try
                {
                    // transformando o body em um array
                    byte[] body = ea.Body.ToArray();

                    // transformando o body em string
                    var message = Encoding.UTF8.GetString(body);
                    var projeto = JsonConvert.DeserializeObject<ProdutosDisponiveis>(message);

                    // Estará realizando a operação de adicição dos projetos no banco de dados
                    for (int i = 0; i <= channel.MessageCount(filaConsumerDisponiveis); i++)
                    {
                        await _repo.adicionarProdutos(projeto);
                    }

                    Console.WriteLine("--> Consumido mensagem vindo da fila [produtos.disponiveis]");
                    Console.WriteLine(message);
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);

                }
                catch (Exception e)
                {
                    channel.BasicNack(ea.DeliveryTag,
                    multiple: false,
                    requeue: true);
                    Console.WriteLine(e);
                }
            };
            // Consome o evento
            channel.BasicConsume(queue: filaConsumerDisponiveis,
                         autoAck: false,
             consumer: consumer);
        }
        void consumirProdutosDeletados(IModel channel)
        {
            // Definindo um consumidor
            var consumer = new EventingBasicConsumer(channel);

            // Definindo o que o consumidor recebe
            consumer.Received += (model, ea) =>
            {
                try
                {
                    // transformando o body em um array
                    byte[] body = ea.Body.ToArray();

                    // transformando o body em string
                    var message = Encoding.UTF8.GetString(body);
                    var produto = JsonConvert.DeserializeObject<ProdutosDisponiveis>(message);

                    // Estará realizando a operação de adicição dos projetos no banco de dados
                    for (int i = 0; i <= channel.MessageCount(filaConsumerDeletados); i++)
                    {
                        _repo.removerProdutos(produto.Id);
                    }
                    Console.WriteLine("--> Consumido mensagem vindo da fila [produtos.disponiveis.deletados]");
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);

                }
                catch (Exception e)
                {
                    channel.BasicNack(ea.DeliveryTag,
                    multiple: false,
                    requeue: true);
                    Console.WriteLine(e);
                }
            };
            // Consome o evento
            channel.BasicConsume(queue: filaConsumerDeletados,
                         autoAck: false,
             consumer: consumer);
        }

        void consumirProdutosAtualizados(IModel channel)
        {
            // Definindo um consumidor
            var consumer = new EventingBasicConsumer(channel);

            // Definindo o que o consumidor recebe
            consumer.Received += async (model, ea) =>
            {
                try
                {
                    // transformando o body em um array
                    byte[] body = ea.Body.ToArray();

                    // transformando o body em string
                    var message = Encoding.UTF8.GetString(body);
                    var produto = JsonConvert.DeserializeObject<ProdutosDisponiveis>(message);

                    // Estará realizando a operação de adicição dos projetos no banco de dados
                    for (int i = 0; i <= channel.MessageCount(filaConsumerAtualizados); i++)
                    {
                        await _repo.atualizarProdutos(produto.Id, produto);
                    }
                    Console.WriteLine("--> Consumido mensagem vindo da fila [produtos.disponiveis.atualizados]");
                    Console.WriteLine(message);
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);

                }
                catch (Exception e)
                {
                    channel.BasicNack(ea.DeliveryTag,
                    multiple: false,
                    requeue: true);
                    Console.WriteLine(e);
                }
            };
            // Consome o evento
            channel.BasicConsume(queue: filaConsumerAtualizados,
                         autoAck: false,
             consumer: consumer);
        }

        void RabbitMQFailed(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine($"--> Não foi possivel se conectar ao Message Bus: {e}");
        }

    }

}