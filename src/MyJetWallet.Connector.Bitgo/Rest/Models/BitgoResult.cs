namespace MyJetWallet.Connector.Bitgo.Rest.Models
{
    public class BitgoResult<T>
    {
        public bool Success { get; set; }
        public T Result { get; set; }
        public BitgoError Error { get; set; }
    }
}