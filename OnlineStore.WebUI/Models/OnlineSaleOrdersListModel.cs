using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.WebUI.Models
{
    public class OnlineSaleOrdersListModel
    {
        public OnlineSaleOrdersListModel()
        {
            OnlineSalePurchasedProducts = new List<OnlineSaleProduct>();
        }
        public int Id { get; set; }
        public int Orderno { get; set; }
        public string OrderRef { get; set; }
        public string AmazonOrderNo { get; set; }
        public string OrderStatus { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Salutation { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string Email { get; set; }
        public string Postcode { get; set; }
        public string ContactNumber { get; set; }
        public int? JobId { get; set; }
        public string JobNo { get; set; }
        public string SSN { get; set; }
        public int? AssetId { get; set; }
        public List<OnlineSaleProduct> OnlineSalePurchasedProducts { get; set; }
        public string OnlineSalePurchasedProductsDesc { get; set; }
    }
}