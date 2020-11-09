using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.WebUI.Models
{
    public class ShipStoreOrderModel
    {
        public ShipStoreOrderModel()
        {
            Products = new List<StoreProductsModel>();
        }
        public int Orderno { get; set; }
        public List<StoreProductsModel> Products { get; set; }
    }
}