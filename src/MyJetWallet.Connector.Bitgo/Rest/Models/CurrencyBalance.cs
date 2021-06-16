using System.Text.Json.Serialization;

namespace MyJetWallet.Connector.Bitgo.Rest.Models
{
    public class CurrencyBalance
    {
        [JsonPropertyName("currencyId")] public string CurrencyId { get; set; }

        [JsonPropertyName("currency")] public string Currency { get; set; }

        [JsonPropertyName("balance")] public string Balance { get; set; }

        [JsonPropertyName("heldBalance")] public string HeldBalance { get; set; }

        [JsonPropertyName("tradableBalance")] public string TradableBalance { get; set; }
    }
}