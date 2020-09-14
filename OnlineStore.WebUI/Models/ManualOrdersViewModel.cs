using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.WebUI.Models
{
    public class ManualOrdersViewModel
    {
        public int Id { get; set; }       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }      
        public string Salutation { get; set; }
        public string Locality { get; set; }
        public string Postcode { get; set; }
        public string State { get; set; }
        public string SKU { get; set; }
        public int? JOBID { get; set; }
        public int? ContactNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string OrderType { get; set; }
        public DateTime? OrderUpdateDate { get; set; }
        public string RefNo { get; set; }
        public bool? IsExport { get; set; }
        public string Comments { get; set; }
        public SelectList CustomerProject { get; set; }
    }
}