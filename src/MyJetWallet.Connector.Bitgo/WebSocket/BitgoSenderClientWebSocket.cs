using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MyJetWallet.Connector.Bitgo.WebSocket.Models.Enums;

namespace MyJetWallet.Connector.Bitgo.WebSocket
{
    [SuppressMessage("ReSharper", "RedundantAnonymousTypePropertyName")]
    public static class BitgoSenderClientWebSocket
    {
        public static async Task SubscribeBitgoChannel(this ClientWebSocket webSocket, string accountId, string channel,
            string productId)
        {
            var msg = JsonSerializer.Serialize(new
            {
                type = MessageType.subscribe.ToString(), accountId = accountId, channel = channel, productId = productId
            });

            await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(msg)), WebSocketMessageType.Text,
                true, CancellationToken.None);
        }
        public static async Task UnsubscribeBitgoChannel(this ClientWebSocket webSocket, string accountId, string channel,
            string productId)
        {
            var msg = JsonSerializer.Serialize(new
            {
                type = MessageType.unsubscribe.ToString(), accountId = accountId, channel = channel, productId = productId
            });

            await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(msg)), WebSocketMessageType.Text,
                true, CancellationToken.None);
        }
    }
}