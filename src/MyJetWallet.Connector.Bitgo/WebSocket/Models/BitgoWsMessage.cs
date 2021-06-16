using System;
using System.Diagnostics.CodeAnalysis;
using MyJetWallet.Connector.Bitgo.WebSocket.Models.Enums;

namespace MyJetWallet.Connector.Bitgo.WebSocket.Models
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class BitgoWsMessage
    {
        public string type { get; set; }
        public string channel { get; set; }
        public string time { get; set; }
        public string message { get; set; }
    }
}