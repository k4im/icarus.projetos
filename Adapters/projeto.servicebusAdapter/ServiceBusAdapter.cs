namespace projeto.servicebusAdapter;
public class ServiceBusAdapter : Base, IServiceBusAdapter
{
    readonly IServiceBusPort _connector;
    readonly IConnection _connection;
    readonly IModel _channel;
    
    public ServiceBusAdapter(IServiceBusPort connector)
    {
        _connector = connector;
        var portaDeConexa = _connector.EfetuarConexaoComRabbitMQ();
        try
        {
            //Criando a conexão ao broker  
            _connection = portaDeConexa.CreateConnection();
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

    public void EnviarEnvelope(Projeto evento)
        => Enviar(SerializarObjeto(evento), routingKey);

    void Enviar(string evento, string routingKeys)
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
    void RabbitMQFailed(object sender, ShutdownEventArgs e)
        => Console.WriteLine("--> RabbitMQ foi derrubado");

    string SerializarObjeto(Projeto evento)
    {
        var projetoModel = new EnvelopeDTO(evento.ProdutoUtilizadoId, 
        evento.QuantidadeUtilizado, "TesteCorrelation");
        return JsonConvert.SerializeObject(projetoModel);
    }
}