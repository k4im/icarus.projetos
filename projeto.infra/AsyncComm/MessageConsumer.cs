namespace projeto.infra.AsyncComm;
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
            CriarFilas(_channel);
            _connection.ConnectionShutdown += RabbitMQFailed;
        }
        catch (Exception e)
        {
            Console.WriteLine($"--> Não foi possivel se conectar ao Message Bus: {e.Message}");
        }
    }

    public void VerificarFila()
        => ConsumirFila(_channel);

    void ConsumirFila(IModel channel)
    {
        var filaComMensagens = VerificarFilaComMensagens();
        // Definindo um consumidor
        var consumer = new EventingBasicConsumer(channel);

        if (filaComMensagens != "vazia")
        {
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
                    for (int i = 0; i <= channel.MessageCount(filaComMensagens); i++)
                    {
                        await LogicaDeFilas(produto, filaComMensagens);
                    }
                    Console.WriteLine($"--> Consumido mensagem vindo da fila [{filaComMensagens}]");
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
            channel.BasicConsume(queue: filaComMensagens,
                         autoAck: false,
             consumer: consumer);
        }
        Console.WriteLine("Fila se encontra vazia");
    }
    string VerificarFilaComMensagens()
    {
        if (_channel.MessageCount(filaConsumerDisponiveis) != 0) return filaConsumerDisponiveis;
        if (_channel.MessageCount(filaConsumerAtualizados) != 0) return filaConsumerAtualizados;
        if (_channel.MessageCount(filaConsumerDeletados) != 0) return filaConsumerDeletados;
        return "vazia";
    }
    async Task LogicaDeFilas(ProdutosDisponiveis model, string fila)
    {
        switch (fila)
        {
            case "produtos.disponiveis":
                await _repo.AdicionarProdutos(model);
                break;
            case "produtos.disponiveis.atualizados":
                await _repo.AtualizarProdutos(model.Id, model);
                break;
            case "produtos.disponiveis.deletados":
                await _repo.RemoverProdutos(model.Id);
                break;
            default:
                break;
        }
    }

    void RabbitMQFailed(object sender, ShutdownEventArgs e)
        => Console.WriteLine($"--> Não foi possivel se conectar ao Message Bus: {e}");
}

