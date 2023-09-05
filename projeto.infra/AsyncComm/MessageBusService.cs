namespace projeto.infra.AsyncComm;
public class MessageBusService : MessageBusServiceExtension, IMessageBusService
{
    readonly IConfiguration _config;
    readonly IConnection _connection;
    readonly IModel _channel;
    public MessageBusService(IConfiguration config)
    {
        _config = config;

        // Definindo a ConnectionFactory, passando o hostname, user, pwd, port
        var factory = new ConnectionFactory()
        {
            HostName = _config["RabbitMQ"],
            UserName = Environment.GetEnvironmentVariable("Rabbit_MQ_User"),
            Password = Environment.GetEnvironmentVariable("Rabbit_MQ_Pwd"),
            Port = int.Parse(_config["RabbitPort"])
        };
        try
        {
            //Criando a conexão ao broker
            _connection = factory.CreateConnection();
            // Criando o modelo da conexão
            _channel = _connection.CreateModel();
            CriarFilas(_channel);
            _connection.ConnectionShutdown += RabbitMQFailed;
        }
        catch (Exception e)
        {
            Console.WriteLine($"--> Não foi possivel se conectar com o TA BINMessage Bus: {e.Message}");
        }
    }

    // Metodo de publicação de um novo projeto contendo todos os dados do projeto
    public void EnviarProjeto(Projeto evento)
        => Enviar(SerializarObjeto(evento), routingKey);

    // Metodo privado de envio da mensagem
    private void Enviar(string evento, string routingKeys)
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
        => Console.WriteLine("--> RabbitMQ foi derrubado");
    static string SerializarObjeto(Projeto evento)
    {
        var projetoModel = new Envelope<Projeto>(evento, "correlationID");
        return JsonConvert.SerializeObject(projetoModel);
    }
}
