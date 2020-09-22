using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.WebUI.Models
{
    public class MainPageProductMonitorModel
    {
        public MainPageProductMonitorModel()
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
        public string ItemTypeName { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string SKU { get; set; }
        public List<string> Images { get; set; }
    }
}