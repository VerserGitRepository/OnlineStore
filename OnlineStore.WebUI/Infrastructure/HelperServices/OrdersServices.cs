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
    public class OrdersServices
    {
        public static readonly string BaseUri = ConfigurationManager.AppSettings["AMSBaseURL"];
        public async static Task<bool> ProcessManualOrders(ManualOrdersViewModel manualordermodel)
        {
            bool returnmessage =false;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.PostAsJsonAsync("OnlineSaleOrders/CreateandProcessManualOrder", manualordermodel).Result;
                if (response.IsSuccessStatusCode)
                {
                    returnmessage = await response.Content.ReadAsAsync<bool>();                    
                }
            }
            return returnmessage;
        }

        public async static Task<bool> AddUpdateProduct(OnlineSaleProduct OnlineSaleProductmodel)
        {
            bool returnmessage = false;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.PostAsJsonAsync("OnlineSaleOrders/AddUpdateProduct", OnlineSaleProductmodel).Result;
                if (response.IsSuccessStatusCode)
                {
                    returnmessage = await response.Content.ReadAsAsync<bool>();
                }
            }
            return returnmessage;
        }

        public async static Task<List<OnlineSaleOrdersListModel>> OnlineSaleOrdersList()
        {
            var returnmessage = new  List<OnlineSaleOrdersListModel>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.GetAsync("OnlineSaleOrders/OnlineSaleOrdersList").Result;
                if (response.IsSuccessStatusCode)
                {
                    returnmessage = await response.Content.ReadAsAsync<List<OnlineSaleOrdersListModel>>();
                }
            }
            return returnmessage;
        }
        public async static Task<List<OnlineSaleProduct>> OnlineSaleProductsList()
        {
            var returnmessage = new List<OnlineSaleProduct>();

            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(10);
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.GetAsync("OnlineSaleOrders/OnlineSaleProductsList").Result;
                if (response.IsSuccessStatusCode)
                {
                    returnmessage = await response.Content.ReadAsAsync<List<OnlineSaleProduct>>();
                }
            }
            return returnmessage;
        }        
    }
}