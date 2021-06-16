namespace MyJetWallet.Connector.Bitgo.Rest
{
    public static class BitgoRestApiFactory
    {
        public static BitgoRestApi CreateClient(string apiKey)
        {
            var client = new Client(apiKey);
            var api = new BitgoRestApi(client);

            return api;
        }
    }
}