using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkomatic.Amazon.SellingPartner.Api.Dto
{
    public class OrderTransaction
    {
        public DateTime PurchaseDate { get; set; }

        public string OrderCurrency { get; set; }

        public decimal OrderAmount { get; set; }

        public string[] PaymentMethodDetails { get; set; }

        public string PaymentMethod { get; set; }
    }
}
