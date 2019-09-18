using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.WebUI.Models
{
    public class WelcomeViewModel
    {
        public ApplicationData.Customer Customer { get; set; }
        public ApplicationData.Sale Sale { get; set; }
        
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string EmployeeEmail { get; set; }

        public string EmployeeID { get; set; }
    }
}