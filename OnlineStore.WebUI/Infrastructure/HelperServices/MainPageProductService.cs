using OnlineStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace OnlineStore.WebUI.Infrastructure.HelperServices
{
    public class MainPageProductService
    {
        public static readonly string BaseUri = ConfigurationManager.AppSettings["AMSBaseURL"];
        public static async Task<List<OnlineSaleProduct>> MainPageProductsList()
        {
            var returnmodel = new List<OnlineSaleProduct>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.GetAsync(string.Format("OnlineSaleOrders/OnlineSaleMainPageProducts")).Result;
                if (response.IsSuccessStatusCode)
                {
                    returnmodel = await response.Content.ReadAsAsync<List<OnlineSaleProduct>>();
                }
            }
            return returnmodel;
        }
    }
}