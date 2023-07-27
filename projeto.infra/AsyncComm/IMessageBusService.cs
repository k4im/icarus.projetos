namespace projeto.infra.AsyncComm
{
    public interface IMessageBusService
    {
        void enviarProjeto(Projeto evento);
    }
}