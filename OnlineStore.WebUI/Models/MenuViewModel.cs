using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.WebUI.Models
{
    public class MenuViewModel
    {
        public string CustomerCode { get; set; }
        public string SaleId { get; set; }
        public IEnumerable<ItemType> ItemTypes { get; set; }
        public string SelectedItemType { get; set; }
    }
}