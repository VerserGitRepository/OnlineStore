using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.WebUI.Models
{
    public class MainPageProductsViewModel
    {
        public MainPageProductsViewModel()
        {
            //MainPageMobileProduct = new List<MainPageProductMobileModel>();
            //MainPageLaptopProduct = new List<MainPageProductLaptopModel>();
            //MainPageDesktopProduct = new List<MainPageProductDesktopModel>();
            //MainPageMonitorProduct = new List<MainPageProductMonitorModel>();
            //CarouselProducts = new List<CarouselProductsViewModel>();
            OnlineSaleProductModel = new List<OnlineSaleProduct>();
        }
        //public List<MainPageProductMobileModel> MainPageMobileProduct { get; set; }
        //public List<MainPageProductLaptopModel> MainPageLaptopProduct { get; set; }
        //public List<MainPageProductDesktopModel> MainPageDesktopProduct { get; set; }
        //public List<MainPageProductMonitorModel> MainPageMonitorProduct { get; set; }
        //public List<CarouselProductsViewModel> CarouselProducts { get; set; }

        public List<OnlineSaleProduct> OnlineSaleProductModel { get; set; }

    }
}