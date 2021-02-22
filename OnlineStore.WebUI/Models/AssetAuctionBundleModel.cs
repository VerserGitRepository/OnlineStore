using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
		[Required(ErrorMessage = "BundleTitle Is Mandatory")]
		public string BundleTitle { get; set; }
		public string BundleDescription { get; set; }
		[Required(ErrorMessage = "BundleBaseprice Is Mandatory")]
		public decimal BundleBaseprice { get; set; }
		[Required(ErrorMessage = "HigestBidPrice Is Mandatory")]
		public decimal HigestBidPrice { get; set; }
		[Required(ErrorMessage = "BundleQuickBidPrice Is Mandatory")]
		public decimal BundleQuickBidPrice { get; set; }
		[Required(ErrorMessage = "PostQuickBidPrice Is Mandatory")]
		public decimal PostQuickBidPrice { get; set; }
		[Required(ErrorMessage = "BundleBuyPrice Is Mandatory")]
		public decimal BundleBuyPrice { get; set; }
		public string AuctionStatus { get; set; }
		[Required(ErrorMessage = "Bundle_Auction_StartDate Is Mandatory")]
		public DateTime Bundle_Auction_StartDate { get; set; }
		[Required(ErrorMessage = "Bundle_Auction_EndDate Is Mandatory")]
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