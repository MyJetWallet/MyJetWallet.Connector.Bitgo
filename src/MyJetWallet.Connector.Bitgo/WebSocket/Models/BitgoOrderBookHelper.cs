namespace MyJetWallet.Connector.Bitgo.WebSocket.Models

{
    public static class BitgoOrderBookHelper
    {
        public static double? GetBitgoOrderBookPrice(this string?[] array)
        {
            if (array.Length < 1 || array[0] == null)
            {
                return null;
            }

            return double.Parse(array[0]);
        }

        public static double? GetBitgoOrderBookVolume(this string?[] array)
        {
            if (array.Length < 2 || array[1] == null)
            {
                return null;
            }

            return double.Parse(array[1]);
        }
    }
}