using System.Diagnostics.CodeAnalysis;

namespace MyJetWallet.Connector.Bitgo.WebSocket.Models.Enums
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum MessageType
    {
        subscribe,
        snapshot,
        error
    }
}