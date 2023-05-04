using Catalog.Business.Configuration;
using Catalog.Business.Interfaces;
using RabbitMQ;
using RabbitMQ.Client;

namespace Catalog.Business.Implementation
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly AppSettings _appSettings;
        private readonly Publisher _publisher;

        public RabbitMqService(AppSettings appSettings)
        {
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            _publisher = new Publisher(new ConnectionFactory { HostName = _appSettings.RabbitMqServerSettings.ConnectionString });
        }

        public void SendMessage<T>(string queue, T obj) => _publisher.SendMessage(queue, obj);
    }
}
