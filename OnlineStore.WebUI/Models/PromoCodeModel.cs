using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.WebUI.Models
{
    public class PromoCodeModel
    {
        public PromoCodeModel()
        {
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "PromoCode Is Mandatory")]
        public int ProductID_FK { get; set; }
        [Required(ErrorMessage = "PromoCode Is Mandatory")]
        public string PromoCode { get; set; }
        [Required(ErrorMessage = "DiscountPercent Is Mandatory")]
        public decimal PromoDiscountPercent { get; set; }
        [Required(ErrorMessage = "StartDate Is Mandatory")]
        public DateTime PromoStartDate { get; set; }
        [Required(ErrorMessage = "EndDate Is Mandatory")]
        public DateTime PromoEndDate { get; set; }

        public bool IsValidPromo { get; set; }
    }
}