using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.WebUI.Models
{
    public class BidModel
    {
        public int BidId { get; set; }
        public int FK_Bid_AuctionCustomerID { get; set; }
        public int FK_Bid_AuctionBundleID { get; set; }
        public decimal BidPrice { get; set; }
        public DateTime Bid_TimeStamp { get; set; }
        public string BidUserId { get; set; }
    }
}