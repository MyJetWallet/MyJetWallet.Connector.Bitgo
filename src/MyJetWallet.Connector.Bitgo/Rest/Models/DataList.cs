using System.Text.Json.Serialization;

namespace MyJetWallet.Connector.Bitgo.Rest.Models
{
    public class DataList<T>
    {
        [JsonPropertyName("data")] public T[] Data { get; set; }
    }
}