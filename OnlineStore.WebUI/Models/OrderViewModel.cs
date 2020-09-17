using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.WebUI.Models
{
    public class OrderViewModel
    {

        public OrderViewModel()
        {
            ManualOrdersViewModel = new ManualOrdersViewModel();
            OnlineSaleProduct = new OnlineSaleProduct();
            OnlineSaleOrdersList = new List<OnlineSaleOrdersListModel>();
        }
        public ManualOrdersViewModel ManualOrdersViewModel { get; set; }
        public OnlineSaleProduct OnlineSaleProduct { get; set; }
        public List<OnlineSaleOrdersListModel> OnlineSaleOrdersList { get; set; }
    }
}