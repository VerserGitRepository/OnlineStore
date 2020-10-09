using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.WebUI.Models
{
    public class CheckoutOrdersPaymentModel
    {
        public decimal paymentAmount { get; set; }
        public string PaymentID { get; set; }
        public string cardType { get; set; }
        public string nameOnCard { get; set; }
        public string CardNumber { get; set; }
        public string expmonth { get; set; }
        public int expyear { get; set; }
        public int cvv { get; set; }
        public int payment_OrderID { get; set; }
        public string PaymentStatus { get; set; }
    }
}