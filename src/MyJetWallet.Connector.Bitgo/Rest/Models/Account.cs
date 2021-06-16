using System.Text.Json.Serialization;

namespace MyJetWallet.Connector.Bitgo.Rest.Models
{
    public class Account
    {
        [JsonPropertyName("id")] public string Id { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
    }
}