using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.WebUI.Models
{
    public class AssetAuctionBundleModel
    {
        public AssetAuctionBundleModel()
        {
			BidTime_Price = new List<string>();
			ImageName = new List<string>();		
		}
		public int Id { get; set; }
		public string BundleTitle { get; set; }
		public string BundleDescription { get; set; }
		public decimal BundleBaseprice { get; set; }
		public decimal HigestBidPrice { get; set; }
		public decimal BundleQuickBidPrice { get; set; }
		public decimal PostQuickBidPrice { get; set; }
		public decimal BundleBuyPrice { get; set; }
		public string AuctionStatus { get; set; }
		public DateTime Bundle_Auction_StartDate { get; set; }
		public DateTime Bundle_Auction_EndDate { get; set; }
		public int? Bundle_Auction_BuyerID { get; set; }
		public bool IsAuctionBundleActive { get; set; }
		public int Bid_Id { get; set; }
		public int FK_Bid_AuctionCustomerID { get; set; }
		public int FK_Bid_AuctionBundleID { get; set; }
		public List<string> BidTime_Price { get; set; }
		public int Image_Id { get; set; }
		public List<string> ImageName { get; set; }
		public int FK_AuctionBundleID { get; set; }
		public bool IsBuyAuctionActive { get; set; }
		public string AuctionEndDateStringFormat { get; set; }
		
	}
}