namespace MyJetWallet.Connector.Bitgo.Rest.Util
{
    public class BitgoResponse
    {
        public bool Success { get; set; }
        public string body { get; set; }

        public BitgoResponse(bool success, string body)
        {
            Success = success;
            this.body = body;
        }
    }
}