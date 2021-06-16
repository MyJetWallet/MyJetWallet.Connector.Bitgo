namespace MyJetWallet.Connector.Bitgo.Rest
{
    public class Client
    {
        public string ApiKey { get; }

        public Client()
        {
            ApiKey = "";
        }

        public Client(string apiKey)
        {
            ApiKey = apiKey;
        }

    }
}