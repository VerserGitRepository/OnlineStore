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
    public class DropDownServices
    {
        private static readonly string CostModelAPIURL = ConfigurationManager.AppSettings["AMSBaseURL"];

        public static async Task<List<ListItems>> ProjectList()
        {
            List<ListItems> returnmodel = new List<ListItems>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(CostModelAPIURL);
                HttpResponseMessage response = client.GetAsync(string.Format("inventorycontrol/TechProjects/listitems")).Result;
                if (response.IsSuccessStatusCode)
                {
                    returnmodel = await response.Content.ReadAsAsync<List<ListItems>>();
                }
            }
            return returnmodel;
        }

        public static async Task<List<ListItems>> Makes()
        {
            List<ListItems> returnmodel = new List<ListItems>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(CostModelAPIURL);
                HttpResponseMessage response = client.GetAsync(string.Format("inventorycontrol/makes/listitems")).Result;
                if (response.IsSuccessStatusCode)
                {
                    returnmodel = await response.Content.ReadAsAsync<List<ListItems>>();
                }
            }
            return returnmodel;
        }
        public static async Task<List<ListItems>> itemtypes()
        {
            List<ListItems> returnmodel = new List<ListItems>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(CostModelAPIURL);
                HttpResponseMessage response = client.GetAsync(string.Format("inventorycontrol/itemtypes/listitems")).Result;
                if (response.IsSuccessStatusCode)
                {
                    returnmodel = await response.Content.ReadAsAsync<List<ListItems>>();
                }
            }
            return returnmodel;
        }

        public static async Task<List<ListItems>> ColoursList()
        {
            List<ListItems> returnmodel = new List<ListItems>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(CostModelAPIURL);
                HttpResponseMessage response = client.GetAsync(string.Format("inventorycontrol/colours/listitems")).Result;
                if (response.IsSuccessStatusCode)
                {
                    returnmodel = await response.Content.ReadAsAsync<List<ListItems>>();
                }
            }
            return returnmodel;
        }

        public static async Task<List<ProductModel>> models(int makeId, int itemTypeId)
        {
            List<ProductModel> returnmodel = new List<ProductModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(CostModelAPIURL);
                HttpResponseMessage response = client.GetAsync(string.Format("inventorycontrol/GetModels/{0}/{1}/listitems", makeId, itemTypeId)).Result;
                if (response.IsSuccessStatusCode)
                {
                    returnmodel = await response.Content.ReadAsAsync<List<ProductModel>>();
                }
            }
            return returnmodel;
        }
    }
}