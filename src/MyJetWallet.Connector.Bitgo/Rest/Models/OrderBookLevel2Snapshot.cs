using System.Text.Json.Serialization;

namespace MyJetWallet.Connector.Bitgo.Rest.Models
{
    public class OrderBookLevel2Snapshot
    {
        [JsonPropertyName("time")] public string Time { get; set; }
        [JsonPropertyName("product")] public string Product { get; set; }
        [JsonPropertyName("bids")] public string[][] Bids { get; set; }
        [JsonPropertyName("asks")] public string[][] Asks { get; set; }
    }
}