using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.WebUI.Models
{
    public class ReturnShippedOrdersModel
    {
        public int OrderNo { get; set; }
        public string JobNo { get; set; }
        public string article_id { get; set; }
        public string consignment_id { get; set; }
        public string URL { get; set; }
        public bool IsValidateState { get; set; }
        public string ResultMessage { get; set; }
    }
}