namespace Cart.Business.Configuration.Settings
{
    public class RabbitMqServerSettings
    {
        public string? ConnectionString { get; set; } = string.Empty;
        public string? Queue { get; set; } = string.Empty;
    }
}
