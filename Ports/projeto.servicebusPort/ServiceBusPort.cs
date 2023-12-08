namespace projeto.servicebusPort;
public class ServiceBusPort
{
    public ConnectionFactory EfetuarConexaoComRabbitMQ() {
        var factory = new ConnectionFactory()
        {
            HostName = Environment.GetEnvironmentVariable("RabbitMQ"),
            UserName = Environment.GetEnvironmentVariable("RABBIT_MQ_USER"),
            Password = Environment.GetEnvironmentVariable("RABBIT_MQ_PWD"),
            Port = int.Parse(Environment.GetEnvironmentVariable("RabbitPort"))
        };

        return factory;
    }
}