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
    }
}