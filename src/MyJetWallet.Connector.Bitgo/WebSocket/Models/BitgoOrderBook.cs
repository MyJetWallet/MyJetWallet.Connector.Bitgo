// ReSharper disable InconsistentNaming

using System.Linq;

namespace MyJetWallet.Connector.Bitgo.WebSocket.Models
{
    public class BitgoOrderBook : BitgoWsMessage
    {
        public string product { get; set; }
        public string[][] bids { get; set; }
        public string[][] asks { get; set; }

        public BitgoOrderBook Copy()
        {
            var result = new BitgoOrderBook()
            {
                type = type,
                channel = channel,
                message = message,
                time = time,
                product = product,
                asks = asks.OrderBy(e => e.GetBitgoOrderBookPrice()).ToArray(),
                bids = bids.OrderByDescending(e => e.GetBitgoOrderBookPrice()).ToArray()
            };

            return result;
        }
    }
}