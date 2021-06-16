using System.Text.Json.Serialization;

namespace MyJetWallet.Connector.Bitgo.Rest.Models
{
    public class Trade
    {
        [JsonPropertyName("id")] public string Id { get; set; }
        [JsonPropertyName("orderId")] public string OrderId { get; set; }
        [JsonPropertyName("time")] public string Time { get; set; }
        [JsonPropertyName("product")] public string Product { get; set; }
        [JsonPropertyName("side")] public string Side { get; set; }
        [JsonPropertyName("price")] public string Price { get; set; }
        [JsonPropertyName("quantity")] public string Quantity { get; set; }
        [JsonPropertyName("settled")] public bool Settled { get; set; }
    }
}