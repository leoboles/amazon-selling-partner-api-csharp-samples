using Amazon;
using Amazon.SellingPartner.Auth.Core;
using Amazon.SellingPartner.Auth.HttpClient;
using Amazon.SellingPartner.Core;
using Amazon.SellingPartner.Orders.Client;
using Amazon.SellingPartner.Serialization.NewtonsoftJson;
using Checkomatic.Amazon.SellingPartner.Api.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Checkomatic.Amazon.SellingPartner.Api
{
    public class OrdersClient
    {
        private IAmazonSellingPartnerOrdersClient ordersClient;

        public OrdersClient(string awsKey, string awsSecret, string roleArn, string clientId, string clientSecret, string refreshToken)
        {
            var spEndpoint = AmazonSpApiEndpoint.NorthAmerica.Endpoint.EnsureTrailingSlash();
            var lwaEndpoint = EndpointConstants.LwaToken;
            var region = RegionEndpoint.USEast1;

            IAmazonSecurityTokenCredentialResolver securityTokenResolver = new AmazonSecurityTokenCredentialResolver(spEndpoint, roleArn, awsKey, awsSecret, region);

            var lwaAuthorizationCredentials = new LwaAuthorizationCredentials
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                RefreshToken = refreshToken,
                Endpoint = new Uri(lwaEndpoint),
                Scopes = null
            };

            ILwaClient lwaClient = new HttpLwaClient(lwaAuthorizationCredentials);

            AmazonSpAccessTokenHandler pipeline = new AmazonSpAccessTokenHandler(lwaClient)
            {
                InnerHandler = new AmazonSpSecurityTokenHandler(securityTokenResolver)
                {
                    InnerHandler = new HttpClientHandler()
                }
            };

            var httpClient = new HttpClient(pipeline)
            {
                BaseAddress = new Uri(spEndpoint)
            };

            this.ordersClient = new AmazonSellingPartnerOrdersClient(httpClient)
            {
                JsonSerializerSettings =
                {
                    // needed if some required fields are not going to be returned due to PII restrictions
                    ContractResolver = new AmazonSellingPartnerSafeContractResolver()
                }
            };
        }

        public async Task<IEnumerable<OrderTransaction>> GetOrderFrom(DateTime startDateTime)
        {
            var result = new List<OrderTransaction>();

            var ordersResponse = await ordersClient.GetOrdersAsync(new[] { AmazonMarketplace.USA.MarketplaceId }, createdAfter: startDateTime.ToAmazonDateTimeString());

            if (ordersResponse.Errors?.Any() ?? false)
            {
                throw new AggregateException(ordersResponse.Errors.Select(e => new Exception(e.Message)));
            }

            bool readMore;
            do
            {
                readMore = false;

                result.AddRange(
                    ordersResponse
                    .Payload
                    .Orders
                    .Where(o => o.OrderTotal != null)
                    .Select(o => new OrderTransaction
                    {
                        OrderAmount = decimal.Parse(o.OrderTotal.Amount),
                        OrderCurrency = o.OrderTotal.CurrencyCode,
                        PaymentMethod = o.PaymentMethod?.ToString(),
                        PaymentMethodDetails = o.PaymentMethodDetails?.ToArray() ?? new string[] { },
                        PurchaseDate = o.PurchaseDate.ToDateTime(),
                    }));

                if (!string.IsNullOrEmpty(ordersResponse.Payload.NextToken))
                {
                    ordersResponse = await ordersClient.GetOrdersAsync(new[] { AmazonMarketplace.USA.MarketplaceId }, nextToken: ordersResponse.Payload.NextToken);
                    readMore = true;
                }
            } while (readMore);

            return result;
        }
    }
}
