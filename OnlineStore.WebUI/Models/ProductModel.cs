using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.WebUI.Models
{
    public class ProductModel
    {
        public int ID { get; set; }
        public string ModelName { get; set; }
        public string ItemTypeId { get; set; }
        public string MakeId { get; set; }
    }
}