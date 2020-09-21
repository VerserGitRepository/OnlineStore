using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.WebUI.Models
{
    public class OnlineSaleProduct
    {
        public OnlineSaleProduct()
        {
            Images = new List<string>();
        }
        public int Id { get; set; }
        public byte[] ProductImage { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public bool IsSKU { get; set; }       
        public int? ItemType_ID { get; set; }
        public int? Brand_ID { get; set; }
        public int? Model_ID { get; set; }
        public decimal PriceExGST { get; set; }
        public decimal GSTAmount { get; set; }
        public decimal PriceIncGST { get; set; }
        public int QtyAvailable { get; set; }
        public string CreatedBy { get; set; }
        public SelectList ItemTypes { get; set; }
        public SelectList Makes { get; set; }
        public string SKU { get; set; }
        public SelectList Models { get; set; }
        public List<string> Images { get; set; }
        public HttpPostedFileBase[] files { get; set; }
    }
}
