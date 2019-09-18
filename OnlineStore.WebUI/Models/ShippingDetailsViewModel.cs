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
            this.States = new List<string> { "ACT", "NSW", "NT", "QLD", "SA", "TAS", "VIC", "WA" };
        }

        [Required(ErrorMessage="Please enter your first name")]
        [Display(Name="First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage="Please enter your last name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter Mobile Number")]
        [Display(Name = "Mobile Number")]

        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage="Please confirm your email address")]
        [System.ComponentModel.DataAnnotations.Compare("Email", ErrorMessage = "Email addresses do not match")]
        [Display(Name="Confirm Email")]
        public string ConfirmEmail { get; set; }

        [Required(ErrorMessage="Please enter an address")]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name="Address Line 2")]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage="Please enter a suburb")]
        public string Suburb { get; set; }

        [Required(ErrorMessage = "Please select a state")]
        public string State { get; set; }

        public List<string> States { get; set; }

        [Required(ErrorMessage="Please enter a postcode")]
        public string Postcode { get; set; }
    }
}