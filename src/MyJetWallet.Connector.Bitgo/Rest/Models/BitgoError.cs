using System.Text.Json.Serialization;

namespace MyJetWallet.Connector.Bitgo.Rest.Models
{
    public class BitgoError
    {
        [JsonPropertyName("error")] public string Error { get; set; }

        [JsonPropertyName("errorName")] public string ErrorName { get; set; }

        [JsonPropertyName("reqId")] public string ReqId { get; set; }
    }
}