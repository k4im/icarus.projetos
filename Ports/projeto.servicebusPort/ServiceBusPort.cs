namespace projeto.servicebusPort;
public class ServiceBusPort
{
    public ConnectionFactory EfetuarConexaoComRabbitMQ() {
        var factory = new ConnectionFactory()
        {
            HostName = Environment.GetEnvironmentVariable("RABBIT_MQ_HOST"),
            UserName = Environment.GetEnvironmentVariable("RABBIT_MQ_USER"),
            Password = Environment.GetEnvironmentVariable("RABBIT_MQ_PWD"),
            Port = int.Parse(Environment.GetEnvironmentVariable("RABBIT_MQ_PORT"))
        };

        return factory;
    }
}