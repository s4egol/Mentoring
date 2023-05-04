using Catalog.Business.Configuration.Settings;
using Microsoft.Extensions.Options;

namespace Catalog.Business.Configuration
{
    public class AppSettings
    {
        private readonly RabbitMqServerSettings _rabbitMqServerSettings;

        public AppSettings(IOptions<RabbitMqServerSettings> rabbitMqServerSettings)
        {
            _rabbitMqServerSettings = rabbitMqServerSettings?.Value ?? throw new ArgumentNullException(nameof(rabbitMqServerSettings));
        }

        public RabbitMqServerSettings RabbitMqServerSettings => _rabbitMqServerSettings;
    }
}
