using System.Text.Json.Serialization;

namespace MyJetWallet.Connector.Bitgo.Rest.Models
{
    public class Currency
    {
        [JsonPropertyName("id")] public string Id { get; set; }
        [JsonPropertyName("symbol")] public string Symbol { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
    }
}