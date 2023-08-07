namespace projeto.domain;

public class Envelope<T>
{
    public Envelope(T message, string correlationId)
    {
        Message = message;
        CorrelationId = correlationId;
    }

    public T Message { get; }
    public string CorrelationId { get; }
}

