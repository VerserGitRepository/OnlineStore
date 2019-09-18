using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineStore.WebUI.ApplicationData;

namespace OnlineStore.WebUI.Models
{
    public class ProductSummaryViewModel
    {
        public SaleProduct SaleProduct { get; set; }
        public AssetAllocation AssetAllocation { get; set; }
        public bool InStock { get; set; }
    }
}