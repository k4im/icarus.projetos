namespace projeto.infra.AsyncComm;

public class MessageBusService : MessageBusServiceExtension, IMessageBusService
{
    private readonly IConfiguration _config;
    private IConnection _connection;
    private IModel _channel;

    public MessageBusService(IConfiguration config)
    {
        _config = config;

        // Definindo a ConnectionFactory, passando o hostname, user, pwd, port
        var factory = new ConnectionFactory()
        {
            HostName = _config["RabbitMQ"],
            UserName = _config["RabbitMQUser"],
            Password = _config["RabbitMQPwd"],
            Port = int.Parse(_config["RabbitPort"])
        };

        try
        {
            //Criando a conexão ao broker
            _connection = factory.CreateConnection();

            // Criando o modelo da conexão
            _channel = _connection.CreateModel();
            criarFilas(_channel);
            _connection.ConnectionShutdown += RabbitMQFailed;
        }
        catch (Exception e)
        {
            Console.WriteLine($"--> Não foi possivel se conectar com o Message Bus: {e.Message}");
        }
    }

    // Metodo de publicação de um novo projeto contendo todos os dados do projeto
    public void enviarProjeto(Projeto evento)
        => enviar(serializarObjeto(evento), routingKey);


    // Metodo privado de envio da mensagem
    private void enviar(string evento, string routingKeys)
    {
        if (_connection.IsOpen)
        {
            // transformando o json em array de bytes
            var body = Encoding.UTF8.GetBytes(evento);

            // Persistindo a mensagem no broker
            var props = _channel.CreateBasicProperties();
            props.Persistent = true;

            // Realizando o envio para o exchange 
            _channel.BasicPublish(exchange: exchange,
                routingKey: routingKeys,
                basicProperties: props,
                body: body);
        }
    }
    // "Logger" caso de algum erro 
    void RabbitMQFailed(object sender, ShutdownEventArgs e)
    {
        Console.WriteLine("--> RabbitMQ foi derrubado");
    }

    string serializarObjeto(Projeto evento)
    {
        var projetoModel = new ProjetoDTO { ProdutoUtilizado = evento.ProdutoUtilizado, QuantidadeUtilizado = evento.QuantidadeUtilizado };
        return JsonConvert.SerializeObject(projetoModel);
    }
}
