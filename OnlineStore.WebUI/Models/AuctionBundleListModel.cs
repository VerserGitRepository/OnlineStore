using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.WebUI.Models
{
    public class AuctionBundleListModel
    {
		public AuctionBundleListModel()
		{
			ImageNames = new List<string>();
		}
		public int Id { get; set; }
		public byte[] ProductImage { get; set; }
		public string BundleTitle { get; set; }
		public string BundleDescription { get; set; }
		public decimal BundleBaseprice { get; set; }
		public decimal BundleQuickBidPrice { get; set; }
		public decimal BundleBuyPrice { get; set; }
		public string AuctionStatus { get; set; }
		public DateTime Bundle_Auction_StartDate { get; set; }
		public DateTime Bundle_Auction_EndDate { get; set; }
		public bool IsAuctionBundleActive { get; set; }
		public List<string> ImageNames { get; set; }
	}
}