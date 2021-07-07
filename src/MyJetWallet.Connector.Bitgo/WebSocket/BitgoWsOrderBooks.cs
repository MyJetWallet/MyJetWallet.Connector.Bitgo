using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyJetWallet.Connector.Bitgo.WebSocket.Models;
using MyJetWallet.Connector.Bitgo.WebSocket.Models.Enums;
using MyJetWallet.Connector.Bitgo.WsEngine;
using Newtonsoft.Json;

namespace MyJetWallet.Connector.Bitgo.WebSocket
{
    [SuppressMessage("ReSharper", "InconsistentLogPropertyNaming")]
    public class BitgoWsOrderBooks : IDisposable
    {
        private readonly ILogger<BitgoWsOrderBooks> _logger;
        private BitgoWebsocketEngine _engine;

        public static string Url { get; set; } = "wss://app.bitgo-test.com/api/prime/trading/v1/ws";

        private readonly Dictionary<string, BitgoOrderBook> _data = new();
        private readonly object _sync = new();

        private readonly IReadOnlyCollection<string> _marketList;
        private readonly string _account;

        public BitgoWsOrderBooks(ILogger<BitgoWsOrderBooks> logger, string authToken, string account,
            IReadOnlyCollection<string> marketList)
        {
            _logger = logger;
            _engine = new BitgoWebsocketEngine(nameof(BitgoWsOrderBooks), Url, authToken, 5000, 10000, logger)
            {
                OnReceive = Receive, OnConnect = Connect
            };
            _account = account;
            _marketList = marketList;
        }

        public void Start()
        {
            _engine.Start();
        }

        public void Stop()
        {
            _engine.Stop();
        }

        public BitgoOrderBook GetOrderBookById(string id)
        {
            lock (_sync)
            {
                if (_data.TryGetValue(id, out var orderBook))
                {
                    return orderBook.Copy();
                }

                return null;
            }
        }

        public List<BitgoOrderBook> GetOrderBooks()
        {
            lock (_sync)
            {
                return _data.Values.Select(e => e.Copy()).ToList();
            }
        }

        public Func<BitgoOrderBook, Task> ReceiveUpdates;

        public void Dispose()
        {
            _engine.Stop();
            _engine.Dispose();
        }

        public async Task Reset(string market)
        {
            var webSocket = _engine.GetClientWebSocket();
            if (webSocket == null)
                return;

            await webSocket.UnsubscribeBitgoChannel(_account, Channel.level2.ToString(), market);
            await webSocket.SubscribeBitgoChannel(_account, Channel.level2.ToString(), market);
        }

        public async Task Subscribe(string market)
        {
            var webSocket = _engine.GetClientWebSocket();
            if (webSocket == null)
                return;

            await webSocket.SubscribeBitgoChannel(_account, Channel.level2.ToString(), market);
        }

        public async Task Unsubscribe(string market)
        {
            var webSocket = _engine.GetClientWebSocket();
            if (webSocket == null)
                return;

            await webSocket.UnsubscribeBitgoChannel(_account, Channel.level2.ToString(), market);
        }

        private async Task Connect(ClientWebSocket webSocket)
        {
            lock (_sync)
            {
                _data.Clear();
            }

            foreach (var market in _marketList)
            {
                await webSocket.SubscribeBitgoChannel(_account, Channel.level2.ToString(), market);
            }
        }

        private async Task Receive(ClientWebSocket webSocket, string msg)
        {
            var orderBook = JsonConvert.DeserializeObject<BitgoOrderBook>(msg);

            if (orderBook?.type == MessageType.error.ToString())
            {
                _logger.LogError("Receive Error from FTX web Socket: {message}", orderBook.message);
                return;
            }

            if (orderBook?.channel == Channel.level2.ToString())
            {
                lock (_sync)
                {
                    _data[orderBook.product] = orderBook;
                }

                await OnReceiveUpdates(orderBook);
            }
        }

        private async Task OnReceiveUpdates(BitgoOrderBook orderBook)
        {
            try
            {
                var action = ReceiveUpdates;
                if (action != null)
                    await action.Invoke(orderBook);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception from method OnReceiveUpdates from client code");
            }
        }
    }
}