using Newtonsoft.Json;

namespace Catalog.Business.Models
{
    public class ProductMessage
    {
        [JsonProperty("id")]
        public int Id { get; init; }
        [JsonProperty("name")]
        public string Name { get; init; }
        [JsonProperty("description")]
        public string? Description { get; init; }
        [JsonProperty("price")]
        public float Price { get; init; }
        [JsonProperty("amount")]
        public int Amount { get; init; }
    }
}
