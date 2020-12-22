using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.WebUI.Models
{
    public class LoginModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool IsLoggedIn { get; set; }
        public string Salutation { get; set; }
        public string Addressline1 { get; set; }
        public string Addressline2 { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public int PostCode { get; set; }
        public string Country { get; set; }
        public string ContactNo { get; set; }
        public int Id { get; set; }
        public string UserID { get; set; }
        public bool? IsActive { get; set; }
        public int? LoginFailcount { get; set; }
        public bool? IsAccountLocked { get; set; }
    }
}