using System.Text.Json.Serialization;

namespace MyJetWallet.Connector.Bitgo.Rest.Models
{
    public class Product
    {
        [JsonPropertyName("id")] public string Id { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("baseCurrencyId")] public string BaseCurrencyId { get; set; }
        [JsonPropertyName("baseCurrency")] public string BaseCurrency { get; set; }
        [JsonPropertyName("quoteCurrencyId")] public string QuoteCurrencyId { get; set; }
        [JsonPropertyName("quoteCurrency")] public string QuoteCurrency { get; set; }
        [JsonPropertyName("baseMinSize")] public string BaseMinSize { get; set; }
        [JsonPropertyName("quoteMinSize")] public string QuoteMinSize { get; set; }
        [JsonPropertyName("quoteIncrement")] public string QuoteIncrement { get; set; }
    }
}