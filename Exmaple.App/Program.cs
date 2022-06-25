using Amazon;
using Amazon.SellingPartner.Auth.Core;
using Amazon.SellingPartner.Auth.HttpClient;
using Amazon.SellingPartner.Core;
using Amazon.SellingPartner.Orders.Client;
using Amazon.SellingPartner.Sales.Client;
using Amazon.SellingPartner.Serialization.NewtonsoftJson;
using Checkomatic.Amazon.SellingPartner.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Exmaple.App
{
    class Program
    {
        static void Main(string[] args)
        {
            // get credentials from Selling Partner Portal
            var awsKey = "";
            var awsSecret = "";
            var roleArn = "";
            var clientId = "";
            var clientSecret = "";
            var refreshToken = "";

            OrdersClient c = new OrdersClient(awsKey, awsSecret, roleArn, clientId, clientSecret, refreshToken);

            var result = c.GetOrderFrom(DateTime.Today.AddDays(-7)).Result;
        }
    }
}
