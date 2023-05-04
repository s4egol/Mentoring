namespace Catalog.Business.Interfaces
{
    public interface IRabbitMqService
    {
        void SendMessage<T>(string queue, T obj);
    }
}
