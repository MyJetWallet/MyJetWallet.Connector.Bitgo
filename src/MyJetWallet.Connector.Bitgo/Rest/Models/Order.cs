using System.Text.Json.Serialization;
using MyJetWallet.Connector.Bitgo.Rest.Models.enums;

namespace MyJetWallet.Connector.Bitgo.Rest.Models
{
    public class Order
    {
        [JsonPropertyName("id")] public string Id { get; set; }
        [JsonPropertyName("accountId")] public string AccountId { get; set; }
        [JsonPropertyName("clientOrderId")] public string ClientOrderId { get; set; }
        [JsonPropertyName("time")] public string Time { get; set; }
        [JsonPropertyName("creationDate")] public string CreationDate { get; set; }
        [JsonPropertyName("scheduledDate")] public string ScheduledDate { get; set; }
        [JsonPropertyName("lastFillDate")] public string LastFillDate { get; set; }
        [JsonPropertyName("completionDate")] public string CompletionDate { get; set; }
        [JsonPropertyName("settleDate")] public string SettleDate { get; set; }
        [JsonPropertyName("type")] public string Type { get; set; }
        [JsonPropertyName("status")] public string Status { get; set; }
        [JsonPropertyName("product")] public string Product { get; set; }
        [JsonPropertyName("side")] public string Side { get; set; }
        [JsonPropertyName("quantity")] public string Quantity { get; set; }
        [JsonPropertyName("quantityCurrency")] public string QuantityCurrency { get; set; }
        [JsonPropertyName("filledQuantity")] public string FilledQuantity { get; set; }
        [JsonPropertyName("averagePrice")] public string AveragePrice { get; set; }
    }
}