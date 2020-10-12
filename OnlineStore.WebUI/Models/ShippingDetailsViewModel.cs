using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OnlineStore.WebUI.Models
{
    public class ShippingDetailsViewModel
    {
        public ShippingDetailsViewModel()
        {
            PurchasedSaleProducts = new List<OnlineSaleProduct>();
            this.Billing_States = new List<string> { "ACT", "NSW", "NT", "QLD", "SA", "TAS", "VIC", "WA" };
            this.Shipping_States = new List<string> { "ACT", "NSW", "NT", "QLD", "SA", "TAS", "VIC", "WA" };
            //if (PurchasedSaleProducts.ToList().Count > 0)
            //{
            //    paymentAmount= PurchasedSaleProducts.Sum(e => e.PriceIncGST * PurchasedSaleProducts.ToList().Count); 
            //}           
        }
        [Required(ErrorMessage = "Please enter your first name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter your last name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter Mobile Number")]
        [Display(Name = "Mobile Number")]
        public string ContactNumber { get; set; }
        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please confirm your email address")]
        [System.ComponentModel.DataAnnotations.Compare("Email", ErrorMessage = "Email addresses do not match")]
        [Display(Name = "Confirm Email")]
        public string ConfirmEmail { get; set; }
        public bool IsBillingAddressDifferent { get; set; }
        //Shipping Address
        [Required(ErrorMessage = "Please enter an address")]
        [Display(Name = "Address Line 1")]
        public string Shipping_AddressLine1 { get; set; }
        [Display(Name = "Address Line 2")]
        public string Shipping_AddressLine2 { get; set; }
        [Required(ErrorMessage = "Please enter a suburb")]
        public string Shipping_Suburb { get; set; }
        [Required(ErrorMessage = "Please select a state")]
        public string Shipping_State { get; set; }
        public List<string> Shipping_States { get; set; }
        [Required(ErrorMessage = "Please enter a postcode")]
        public string Shipping_Postcode { get; set; }
        public string OrderType { get; set; }
        //Billing Address       
        public string Billing_AddressLine1 { get; set; }      
        public string Billing_AddressLine2 { get; set; }        
        public string Billing_Suburb { get; set; }       
        public string Billing_State { get; set; }
        public List<string> Billing_States { get; set; }      
        public string Billing_Postcode { get; set; }
        public decimal paymentAmount { get; set; }
        public string PaymentID { get; set; }
        public string cardType { get; set; }
       // [Required(ErrorMessage = "Please enter a Name On Card")]
        public string nameOnCard { get; set; }
       // [Required(ErrorMessage = "Please enter a CardNumber")]
        public string CardNumber { get; set; }
      //  [Required(ErrorMessage = "Please enter a Expire Month")]
        public string expmonth { get; set; }
     //   [Required(ErrorMessage = "Please enter a Expire year")]
        public int expyear { get; set; }
      //  [Required(ErrorMessage = "Please enter a cvv")]
        public int cvv { get; set; }
        public int payment_OrderID { get; set; }
        public string PaymentStatus { get; set; }
        public List<OnlineSaleProduct> PurchasedSaleProducts { get; set; }
       
    }
}