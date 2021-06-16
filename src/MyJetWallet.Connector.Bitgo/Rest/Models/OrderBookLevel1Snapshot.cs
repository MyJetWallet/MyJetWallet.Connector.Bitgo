using System.Text.Json.Serialization;

namespace MyJetWallet.Connector.Bitgo.Rest.Models
{
    public class OrderBookLevel1Snapshot
    {
        [JsonPropertyName("time")] public string Time { get; set; }
        [JsonPropertyName("product")] public string Product { get; set; }
        [JsonPropertyName("bidPrice")] public string BidPrice { get; set; }
        [JsonPropertyName("bidSize")] public string BidSize { get; set; }
        [JsonPropertyName("askPrice")] public string AskPrice { get; set; }
        [JsonPropertyName("askSize")] public string AskSize { get; set; }
    }
}