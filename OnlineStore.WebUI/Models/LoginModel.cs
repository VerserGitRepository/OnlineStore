using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OnlineStore.WebUI.Models
{
    public class LoginModel
    {

        [Required(ErrorMessage = "FirstName Is Mandatory")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName Is Mandatory")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email Is Mandatory")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "UserName Is Mandatory")]
        public string UserName { get; set; }
        public string FullName { get; set; }
        [Required(ErrorMessage = "Password Is Mandatory")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password Is Mandatory")]
        public string ConfirmPassword { get; set; }
        public bool IsLoggedIn { get; set; }
        [Required(ErrorMessage = "Salutation Is Mandatory")]
        public string Salutation { get; set; }
        [Required(ErrorMessage = "Addressline1 Is Mandatory")]
        public string Addressline1 { get; set; }
        [Required(ErrorMessage = "Addressline2 Is Mandatory")]
        public string Addressline2 { get; set; }
        [Required(ErrorMessage = "Suburb Is Mandatory")]
        public string Suburb { get; set; }
        [Required(ErrorMessage = "State Is Mandatory")]
        public string State { get; set; }
        [Required(ErrorMessage = "PostCode Is Mandatory")]
        public int PostCode { get; set; }
        [Required(ErrorMessage = "Countryd Is Mandatory")]
        public string Country { get; set; }
        [Required(ErrorMessage = "ContactNo Is Mandatory")]
        public string ContactNo { get; set; }
        public int Id { get; set; }
        public string UserID { get; set; }
        public bool? IsActive { get; set; }
        public int? LoginFailcount { get; set; }
        public bool? IsAccountLocked { get; set; }
    }
}