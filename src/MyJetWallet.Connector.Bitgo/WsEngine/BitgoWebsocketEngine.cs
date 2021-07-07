using System;
using System.Net.WebSockets;
using Microsoft.Extensions.Logging;

namespace MyJetWallet.Connector.Bitgo.WsEngine
{
    public class BitgoWebsocketEngine : Sdk.WebSocket.WebsocketEngine
    {
        private readonly string _authToken;
        private readonly int _keepAliveInterval;
        
        public BitgoWebsocketEngine(string name, string url, string authToken, int pingIntervalMSec,
            int silenceDisconnectIntervalMSec, ILogger logger) :
            base(name, url, pingIntervalMSec, silenceDisconnectIntervalMSec, logger)
        {
            _authToken = authToken;
            _keepAliveInterval = pingIntervalMSec;
        }

        protected override void InitHeaders(ClientWebSocket clientWebSocket)
        {
            clientWebSocket.Options.SetRequestHeader("Authorization", $"Bearer {_authToken}");
            clientWebSocket.Options.KeepAliveInterval = TimeSpan.FromMilliseconds(_keepAliveInterval);
        }
    }
}