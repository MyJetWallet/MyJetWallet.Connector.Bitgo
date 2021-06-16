using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyJetWallet.Connector.Bitgo.Rest;
using MyJetWallet.Connector.Bitgo.WebSocket;

namespace TestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Console.WriteLine(Environment.GetEnvironmentVariable("API-KEY"));
            // await TestRestApi();
            // await TestRestApiOrder();
            await TestWsOrderBook();
        }

        private static async Task TestWsOrderBook()
        {
            using ILoggerFactory loggerFactory =
                LoggerFactory.Create(builder =>
                    builder. AddSimpleConsole(options =>
                    {
                        options.IncludeScopes = true;
                        options.SingleLine = true;
                        options.TimestampFormat = "hh:mm:ss ";
                    }));

            var client = new BitgoWsOrderBooks(loggerFactory.CreateLogger<BitgoWsOrderBooks>(), "token", "account", new []{ "TBTC-TUSD*" });

            client.Start();

            bool log = true;

            client.ReceiveUpdates = book =>
            {
                if (log)
                    Console.WriteLine($"Receive updates for {book.product}");

                log = false;

                return Task.CompletedTask;
            };

            var cmd = Console.ReadLine();
            while (cmd != "exit")
            {
                if (cmd == "count")
                {
                    var books = client.GetOrderBooks().Count;
                    Console.WriteLine($"Count books: {books}");
                }
                else if (cmd == "reset")
                {
                    client.Reset("TBTC-TUSD*").Wait();
                }
                else if (cmd == "time")
                {
                    var book = client.GetOrderBookById("TBTC-TUSD*");

                    Console.WriteLine($"nw: {DateTimeOffset.UtcNow:O}");
                    Console.WriteLine($"t1: {book.time}");

                    client.Reset("TBTC-TUSD*").Wait();
                }
                else
                {
                    var orderBook = client.GetOrderBookById(cmd);

                    if (orderBook != null)
                        Console.WriteLine($"{orderBook.product} {orderBook.time}  {orderBook.asks.Length}|{orderBook.bids.Length}");
                    else
                        Console.WriteLine("Not found");
                }

                cmd = Console.ReadLine();
            }
        }

        private static async Task TestRestApiOrder()
        {
            var api = BitgoRestApiFactory.CreateClient("token");
            // var orderResponse = await api.PlaceLimitOrder("account", "OrderId123", "TBTC-TUSD*",
            //     OrderSide.buy, 1000, "TUSD*", 10000);
            var orderResponse = await api.CancelOrder("account", "6d092593-6c6a-43f0-8096-0b374553cac5");
            Console.WriteLine(JsonSerializer.Serialize(orderResponse, new JsonSerializerOptions() {WriteIndented = true}));
        }
    
        private static async Task TestRestApi()
        {
            var api = BitgoRestApiFactory.CreateClient("token");
            
            Console.WriteLine(" ====  current user ==== ");
            var currentUser = await api.GetCurrentUser();
            Console.WriteLine(JsonSerializer.Serialize(currentUser, new JsonSerializerOptions() {WriteIndented = true}));
            Console.WriteLine();
            Console.WriteLine();
            
            Console.WriteLine(" ====  list accounts ==== ");
            var accounts = await api.ListAccounts();
            Console.WriteLine(JsonSerializer.Serialize(accounts, new JsonSerializerOptions() {WriteIndented = true}));
            Console.WriteLine();
            Console.WriteLine();
            
            foreach (var account in accounts.Result.Data)
            {
                Console.WriteLine($" ====  balance for account {account.Id}  ==== ");
                var balances = await api.GetAccountBalance(account.Id);
                Console.WriteLine(JsonSerializer.Serialize(balances, new JsonSerializerOptions() {WriteIndented = true}));
                Console.WriteLine();
                Console.WriteLine();
                
                Console.WriteLine($" ====  trades for account {account.Id}  ==== ");
                var trades = await api.ListTrades(account.Id);
                Console.WriteLine(JsonSerializer.Serialize(trades, new JsonSerializerOptions() {WriteIndented = true}));
                Console.WriteLine();
                Console.WriteLine();

                foreach (var trade in trades.Result.Data)
                {
                    Console.WriteLine($" ====  trade for account {account.Id}, trade {trade.Id} ==== ");
                    var tradeDetails = await api.GetTrade(account.Id, trade.Id);
                    Console.WriteLine(JsonSerializer.Serialize(tradeDetails, new JsonSerializerOptions() {WriteIndented = true}));
                    Console.WriteLine();
                    Console.WriteLine();
                }
                
                Console.WriteLine($" ====  currencies for account {account.Id}  ==== ");
                var currencies = await api.ListCurrencies(account.Id);
                Console.WriteLine(JsonSerializer.Serialize(currencies, new JsonSerializerOptions() {WriteIndented = true}));
                Console.WriteLine();
                Console.WriteLine();
                
                Console.WriteLine($" ====  products for account {account.Id}  ==== ");
                var products = await api.ListProducts(account.Id);
                Console.WriteLine(JsonSerializer.Serialize(products, new JsonSerializerOptions() {WriteIndented = true}));
                Console.WriteLine();
                Console.WriteLine();

                foreach (var product in products.Result.Data)
                {
                    Console.WriteLine($" ====  OrderBook Level1 for account {account.Id}, product {product.Name}  ==== ");
                    var level1 = await api.GetOrderBookLevel1(account.Id, product.Name);
                    Console.WriteLine(JsonSerializer.Serialize(level1, new JsonSerializerOptions() {WriteIndented = true}));
                    Console.WriteLine();
                    Console.WriteLine();
                    
                    Console.WriteLine($" ====  OrderBook Level2 for account {account.Id}, product {product.Name}  ==== ");
                    var level2 = await api.GetOrderBookLevel2(account.Id, product.Name);
                    Console.WriteLine(JsonSerializer.Serialize(level2, new JsonSerializerOptions() {WriteIndented = true}));
                    Console.WriteLine();
                    Console.WriteLine();
                }
                
                Console.WriteLine($" ====  orders for account {account.Id}  ==== ");
                var orders = await api.ListOrders(account.Id);
                Console.WriteLine(JsonSerializer.Serialize(orders, new JsonSerializerOptions() {WriteIndented = true}));
                Console.WriteLine();
                Console.WriteLine();

                foreach (var order in orders.Result.Data)
                {
                    Console.WriteLine($" ====  order for account {account.Id}, order {order.Id} ==== ");
                    var orderDetails = await api.GetOrder(account.Id, order.Id);
                    Console.WriteLine(JsonSerializer.Serialize(orderDetails, new JsonSerializerOptions() {WriteIndented = true}));
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
        }
    }
}