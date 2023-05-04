using Cart.Business.Configuration;
using Cart.Business.Interfaces;
using Cart.Business.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ;
using RabbitMQ.Client;

namespace Cart.Business.Implementations
{
    public class RabbitMqListener : BackgroundService
    {
        private readonly AppSettings _appSettings;
        private readonly Subscriber _subscriber;
        private readonly ICartingService _cartService;

        public RabbitMqListener(IServiceScopeFactory serviceScopeFactory)
        {
            using var scope = serviceScopeFactory.CreateScope();

            _cartService = scope.ServiceProvider.GetRequiredService<ICartingService>();
            _appSettings = scope.ServiceProvider.GetRequiredService<AppSettings>();
            _subscriber = new Subscriber(
                new ConnectionFactory { HostName = _appSettings.RabbitMqServerSettings.ConnectionString },
                _appSettings.RabbitMqServerSettings.Queue);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            _subscriber.Run((message) =>
            {
                var product = JsonConvert.DeserializeObject<ProductMessage>(message);
                _cartService.UpdateItems(new[] { product });
            });

            return Task.CompletedTask;
        }
    }
}
