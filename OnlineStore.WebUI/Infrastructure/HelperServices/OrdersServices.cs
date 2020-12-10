using OnlineStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.WebUI.Infrastructure.HelperServices
{
    public class OrdersServices
    {
        public static readonly string BaseUri = ConfigurationManager.AppSettings["AMSBaseURL"];

        public async static Task<OnlineSaleProduct> OnlineSaleShippedOrder()
        {
            var returnmessage = new OnlineSaleProduct();
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(10);
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.GetAsync("OnlineSaleOrders/OnlineSaleOrdersList").Result;
                if (response.IsSuccessStatusCode)
                {
                    returnmessage = await response.Content.ReadAsAsync<OnlineSaleProduct>();
                }
            }
            return returnmessage;
        }
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

        public async static Task<ReturnValidationModel> OnlineStoreCheckoutOrder(ShippingDetailsViewModel _checkoutDataModel)
        {
            var returnmessage =new ReturnValidationModel();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.PostAsJsonAsync("OnlineSaleOrders/CreateStoreCheckoutOrder", _checkoutDataModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    returnmessage = await response.Content.ReadAsAsync<ReturnValidationModel>();
                }
            }
            return returnmessage;
        }

        public async static Task<ReturnValidationModel> CheckoutOrdersPaymentRequest(CheckoutOrdersPaymentModel _checkoutpaymentDataModel)
        {
            var returnmessage =new ReturnValidationModel();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.PostAsJsonAsync("OnlineSaleOrders/CheckoutOrdersPaymentRequest", _checkoutpaymentDataModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    returnmessage = await response.Content.ReadAsAsync<ReturnValidationModel>();
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
        [OutputCache(CacheProfile = "Cache5Min")]
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

        [OutputCache(CacheProfile = "Cache5Min")]
        public async static Task<List<OnlineSaleProduct>> OnlineSaleProduct()
        {
            var returnmessage = new List<OnlineSaleProduct>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.GetAsync("OnlineSaleOrders/OnlineStoreSaleOrdersList").Result;
                if (response.IsSuccessStatusCode)
                {
                    returnmessage = await response.Content.ReadAsAsync<List<OnlineSaleProduct>>();
                }
            }
            return returnmessage;
        }
        [OutputCache(CacheProfile = "Cache5Min")]
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

        public async static Task<OnlineSaleProduct> OnlineSaleProductById(int ProductId)
        {
            var returnmessage = new OnlineSaleProduct();
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(10);
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.GetAsync(string.Format("OnlineSaleOrders/{0}/OnlineSaleProductById", ProductId)).Result;
                if (response.IsSuccessStatusCode)
                {
                    returnmessage = await response.Content.ReadAsAsync<OnlineSaleProduct>();
                }
            }
            return returnmessage;
        }
        public async static Task<ReturnShippedOrdersModel> SendShipment(ShipStoreOrderModel shipmentModel)
        {
            var returnModel = new ReturnShippedOrdersModel();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.PostAsJsonAsync("OnlineStoreShipment/EParcelShipmentAndLabelOrder", shipmentModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    returnModel = await response.Content.ReadAsAsync<ReturnShippedOrdersModel>();
                }
            }
            return returnModel;
        }
        public async static Task<bool> ApplyPromoCode(PromoCodeModel model)
        {
            var returnModel = new ReturnShippedOrdersModel();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.PostAsJsonAsync("OnlineSaleOrders/CreateSalePromoCode", model).Result;
                if (response.IsSuccessStatusCode)
                {
                    returnModel = await response.Content.ReadAsAsync<ReturnShippedOrdersModel>();
                }
            }
            return true;
        }
        public async static Task<PromoCodeModel> ApplyPromoCode(string promoCode)
        {
            var returnmessage = new PromoCodeModel();
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(10);
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.GetAsync(string.Format("OnlineSaleOrders/{0}/PromoCodeValidation",promoCode)).Result;
                if (response.IsSuccessStatusCode)
                {
                    returnmessage = await response.Content.ReadAsAsync<PromoCodeModel>();
                }
            }
            return returnmessage;
        }

        public async static Task<string> CreateOnlineStoreUserAccount(LoginModel CreateNewUser)
        {
            string returnmessage=string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(10);
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage response = client.PostAsJsonAsync("SalesList/CreateOnlineStoreUserAccount", CreateNewUser).Result;
                if (response.IsSuccessStatusCode)
                {
                    returnmessage = await response.Content.ReadAsAsync<string>();
                }
            }
            return returnmessage;
        }
    }
}