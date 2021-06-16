using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MyJetWallet.Connector.Bitgo.Rest.Models;
using MyJetWallet.Connector.Bitgo.Rest.Models.enums;
using MyJetWallet.Connector.Bitgo.Rest.Util;

namespace MyJetWallet.Connector.Bitgo.Rest
{
    public class BitgoRestApi
    {
        private const string Url = "https://app.bitgo-test.com/";

        private readonly Client _client;

        private readonly HttpClient _httpClient;

        private long _nonce;

        public BitgoRestApi(Client client)
        {
            _client = client;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(Url),
                Timeout = TimeSpan.FromSeconds(30)
            };
        }

        #region Account

        public async Task<BitgoResult<User>> GetCurrentUser()
        {
            var apiPath = $"api/prime/trading/v1/user/current";
            var result = await CallAsync(HttpMethod.Get, apiPath);
            return new BitgoResult<User>()
            {
                Success = result.Success,
                Result = result.Success ? JsonSerializer.Deserialize<User>(result.body) : null,
                Error = result.Success ? null : JsonSerializer.Deserialize<BitgoError>(result.body)
            };
        }

        public async Task<BitgoResult<DataList<Account>>> ListAccounts()
        {
            var apiPath = $"api/prime/trading/v1/accounts";
            var result = await CallAsync(HttpMethod.Get, apiPath);
            return new BitgoResult<DataList<Account>>()
            {
                Success = result.Success,
                Result = result.Success ? JsonSerializer.Deserialize<DataList<Account>>(result.body) : null,
                Error = result.Success ? null : JsonSerializer.Deserialize<BitgoError>(result.body)
            };
        }

        public async Task<BitgoResult<DataList<CurrencyBalance>>> GetAccountBalance(string accountId)
        {
            var apiPath = $"api/prime/trading/v1/accounts/{accountId}/balances";
            var result = await CallAsync(HttpMethod.Get, apiPath);
            return new BitgoResult<DataList<CurrencyBalance>>()
            {
                Success = result.Success,
                Result = result.Success ? JsonSerializer.Deserialize<DataList<CurrencyBalance>>(result.body) : null,
                Error = result.Success ? null : JsonSerializer.Deserialize<BitgoError>(result.body)
            };
        }

        #endregion

        #region Orders

        public async Task<BitgoResult<Order>> PlaceLimitOrder(string accountId, string clientOrderId,
            string product, OrderSide side, decimal quantity, string quantityCurrency, decimal limitPrice)
        {
            var apiPath = $"api/prime/trading/v1/accounts/{accountId}/orders";

            var body =
                $"{{\"clientOrderId\": \"{clientOrderId}\"," +
                $"\"product\": \"{product}\"," +
                $"\"type\": \"limit\"," +
                $"\"side\": \"{side}\"," +
                $"\"quantity\": \"{quantity}\"," +
                $"\"quantityCurrency\": \"{quantityCurrency}\"," +
                $"\"limitPrice\": \"{limitPrice}\"}}";

            var result = await CallAsync(HttpMethod.Post, apiPath, body);
            return new BitgoResult<Order>()
            {
                Success = result.Success,
                Result = result.Success ? JsonSerializer.Deserialize<Order>(result.body) : null,
                Error = result.Success ? null : JsonSerializer.Deserialize<BitgoError>(result.body)
            };
        }
        
        public async Task<BitgoResult<Order>> PlaceMarketOrder(string accountId, string clientOrderId,
            string product, OrderSide side, decimal quantity, string quantityCurrency)
        {
            var apiPath = $"api/prime/trading/v1/accounts/{accountId}/orders";

            var body =
                $"{{\"clientOrderId\": \"{clientOrderId}\"," +
                $"\"product\": \"{product}\"," +
                $"\"type\": \"market\"," +
                $"\"side\": \"{side}\"," +
                $"\"quantity\": \"{quantity}\"," +
                $"\"quantityCurrency\": \"{quantityCurrency}\"}}";

            var result = await CallAsync(HttpMethod.Post, apiPath, body);
            return new BitgoResult<Order>()
            {
                Success = result.Success,
                Result = result.Success ? JsonSerializer.Deserialize<Order>(result.body) : null,
                Error = result.Success ? null : JsonSerializer.Deserialize<BitgoError>(result.body)
            };
        }

        public async Task<BitgoResult<DataList<Order>>> ListOrders(string accountId)
        {
            var apiPath = $"api/prime/trading/v1/accounts/{accountId}/orders";
            var result = await CallAsync(HttpMethod.Get, apiPath);
            return new BitgoResult<DataList<Order>>()
            {
                Success = result.Success,
                Result = result.Success ? JsonSerializer.Deserialize<DataList<Order>>(result.body) : null,
                Error = result.Success ? null : JsonSerializer.Deserialize<BitgoError>(result.body)
            };
        }

        public async Task<BitgoResult<DataList<Order>>> GetOrderByExternalId(string accountId, string externalId)
        {
            var apiPath = $"api/prime/trading/v1/accounts/{accountId}/orders?clientOrderId={externalId}";
            var result = await CallAsync(HttpMethod.Get, apiPath);
            return new BitgoResult<DataList<Order>>()
            {
                Success = result.Success,
                Result = result.Success ? JsonSerializer.Deserialize<DataList<Order>>(result.body) : null,
                Error = result.Success ? null : JsonSerializer.Deserialize<BitgoError>(result.body)
            };
        }

        public async Task<BitgoResult<Order>> GetOrder(string accountId, string orderId)
        {
            var apiPath = $"api/prime/trading/v1/accounts/{accountId}/orders/{orderId}";
            var result = await CallAsync(HttpMethod.Get, apiPath);
            return new BitgoResult<Order>()
            {
                Success = result.Success,
                Result = result.Success ? JsonSerializer.Deserialize<Order>(result.body) : null,
                Error = result.Success ? null : JsonSerializer.Deserialize<BitgoError>(result.body)
            };
        }

        public async Task<BitgoResult<string>> CancelOrder(string accountId, string orderId)
        {
            var apiPath = $"api/prime/trading/v1/accounts/{accountId}/orders/{orderId}/cancel";
            var result = await CallAsync(HttpMethod.Put, apiPath);
            return new BitgoResult<string>()
            {
                Success = result.Success,
                Result = result.Success ? JsonSerializer.Deserialize<string>(result.body) : null,
                Error = result.Success ? null : JsonSerializer.Deserialize<BitgoError>(result.body)
            };
        }

        #endregion

        #region Trades

        public async Task<BitgoResult<DataList<Trade>>> ListTrades(string accountId)
        {
            var apiPath = $"api/prime/trading/v1/accounts/{accountId}/trades";
            var result = await CallAsync(HttpMethod.Get, apiPath);
            return new BitgoResult<DataList<Trade>>()
            {
                Success = result.Success,
                Result = result.Success ? JsonSerializer.Deserialize<DataList<Trade>>(result.body) : null,
                Error = result.Success ? null : JsonSerializer.Deserialize<BitgoError>(result.body)
            };
        }

        public async Task<BitgoResult<Trade>> GetTrade(string accountId, string tradeId)
        {
            var apiPath = $"api/prime/trading/v1/accounts/{accountId}/trades/{tradeId}";
            var result = await CallAsync(HttpMethod.Get, apiPath);
            return new BitgoResult<Trade>()
            {
                Success = result.Success,
                Result = result.Success ? JsonSerializer.Deserialize<Trade>(result.body) : null,
                Error = result.Success ? null : JsonSerializer.Deserialize<BitgoError>(result.body)
            };
        }

        #endregion

        #region MarketData

        public async Task<BitgoResult<DataList<Currency>>> ListCurrencies(string accountId)
        {
            var apiPath = $"api/prime/trading/v1/accounts/{accountId}/currencies";
            var result = await CallAsync(HttpMethod.Get, apiPath);
            return new BitgoResult<DataList<Currency>>()
            {
                Success = result.Success,
                Result = result.Success ? JsonSerializer.Deserialize<DataList<Currency>>(result.body) : null,
                Error = result.Success ? null : JsonSerializer.Deserialize<BitgoError>(result.body)
            };
        }

        public async Task<BitgoResult<DataList<Product>>> ListProducts(string accountId)
        {
            var apiPath = $"api/prime/trading/v1/accounts/{accountId}/products";
            var result = await CallAsync(HttpMethod.Get, apiPath);
            return new BitgoResult<DataList<Product>>()
            {
                Success = result.Success,
                Result = result.Success ? JsonSerializer.Deserialize<DataList<Product>>(result.body) : null,
                Error = result.Success ? null : JsonSerializer.Deserialize<BitgoError>(result.body)
            };
        }

        #endregion

        #region OrderBooks

        public async Task<BitgoResult<OrderBookLevel1Snapshot>> GetOrderBookLevel1(string accountId, string product)
        {
            var apiPath = $"api/prime/trading/v1/accounts/{accountId}/products/{product}/level1";
            var result = await CallAsync(HttpMethod.Get, apiPath);
            return new BitgoResult<OrderBookLevel1Snapshot>()
            {
                Success = result.Success,
                Result = result.Success ? JsonSerializer.Deserialize<OrderBookLevel1Snapshot>(result.body) : null,
                Error = result.Success ? null : JsonSerializer.Deserialize<BitgoError>(result.body)
            };
        }

        public async Task<BitgoResult<OrderBookLevel2Snapshot>> GetOrderBookLevel2(string accountId, string product)
        {
            var apiPath = $"api/prime/trading/v1/accounts/{accountId}/products/{product}/level2";
            var result = await CallAsync(HttpMethod.Get, apiPath);
            return new BitgoResult<OrderBookLevel2Snapshot>()
            {
                Success = result.Success,
                Result = result.Success ? JsonSerializer.Deserialize<OrderBookLevel2Snapshot>(result.body) : null,
                Error = result.Success ? null : JsonSerializer.Deserialize<BitgoError>(result.body)
            };
        }

        #endregion

        #region Util

        private async Task<BitgoResponse> CallAsync(HttpMethod method, string endpoint, string body = null)
        {
            var request = new HttpRequestMessage(method, endpoint);

            if (body != null)
            {
                request.Content = new StringContent(body, Encoding.UTF8, "application/json");
            }

            request.Headers.Add("Authorization", "Bearer " + _client.ApiKey);

            var response = await _httpClient.SendAsync(request).ConfigureAwait(true);

            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return new BitgoResponse(response.IsSuccessStatusCode, result);
        }

        #endregion
    }
}