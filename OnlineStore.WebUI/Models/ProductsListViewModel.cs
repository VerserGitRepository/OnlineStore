 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineStore.WebUI.ApplicationData;

namespace OnlineStore.WebUI.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<SaleProductInfo> SaleProductInfos { get; set; }
        public string SelectedItemType { get; set; }
        public Customer Customer { get; set; }
        public string SaleId { get; set; }
    }
}