using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ
{
    public class Publisher
    {
        private readonly ConnectionFactory _connectionFactory;

        public Publisher(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public void SendMessage<T>(string queue, T obj)
        {
            ArgumentNullException.ThrowIfNull(obj, nameof(obj));

            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));

            channel.BasicPublish(exchange: string.Empty,
                routingKey: queue,
                basicProperties: null,
                body: body);
        }
    }
}