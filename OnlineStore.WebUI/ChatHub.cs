using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using OnlineStore.WebUI.Infrastructure.HelperServices;
using OnlineStore.WebUI.Models;

namespace OnlineStore.WebUI
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message,int id, int bundleid,decimal bidPrice,int customerId)
        {
            BidModel model = new BidModel();
           // model.BidId = id;
            model.BidPrice = bidPrice;
            //model.BidUserId = name;
            model.Bid_TimeStamp = DateTime.Now;
            model.FK_Bid_AuctionBundleID = bundleid;
            model.FK_Bid_AuctionCustomerID = customerId;

            bool result = OrdersServices.PostBid(model).Result;
            if (result)
            {
                Clients.All.addNewMessageToPage(name, message);
            }
            
            
        }
    }
}