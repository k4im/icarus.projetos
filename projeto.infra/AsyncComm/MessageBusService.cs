namespace projeto.infra.AsyncComm;
public class MessageBusService : MessageBusServiceExtension, IMessageBusService
{
    readonly IConfiguration _config;
    readonly IConnection _connection;
    readonly IModel _channel;
    readonly HttpContent _content;
    public MessageBusService(IConfiguration config, HttpContent content)
    {
        _config = config;
        _content = content;

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
    string SerializarObjeto(Projeto evento)
    {
        var correlationId = _content.Headers.GetValues("X-CorrelationID").FirstOrDefault();
        var projetoModel = new Envelope<Projeto>(evento, correlationId);
        return JsonConvert.SerializeObject(projetoModel);
    }
}
