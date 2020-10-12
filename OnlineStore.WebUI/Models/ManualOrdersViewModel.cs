using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.WebUI.Models
{
    public class ManualOrdersViewModel
    {
        public ManualOrdersViewModel()
        {
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "FirstName Is Mandatory")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Surname Is Mandatory")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "AddressLine1 Is Mandatory")]
        public string AddressLine1 { get; set; }
        [Required(ErrorMessage = "AddressLine2 Is Mandatory")]
        public string AddressLine2 { get; set; }
        [Required(ErrorMessage = "Saluation Is Mandatory")]
        public string Salutation { get; set; }
        [Required(ErrorMessage = "Locality Is Mandatory")]
        public string Locality { get; set; }
        [Required(ErrorMessage = "Postcode Is Mandatory")]
        public string Postcode { get; set; }
        [Required(ErrorMessage = "State Is Mandatory")]
        public string State { get; set; }
        [Required(ErrorMessage = "Email Is Mandatory")]
        public string Email { get; set; }
        [Required(ErrorMessage = "ContactNumber Is Mandatory")]
        public int? ContactNumber { get; set; }
        [Required(ErrorMessage = "SKU Is Mandatory")]
        public string SKU { get; set; }
        public int? JOBID { get; set; }       
        public DateTime? OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string OrderType { get; set; }
        public DateTime? OrderUpdateDate { get; set; }
        public string RefNo { get; set; }
        public bool? IsExport { get; set; }
        public string Comments { get; set; }
        public SelectList CustomerProject { get; set; }
        public int? ProjectId { get; set; }
        [Required(ErrorMessage = "SSN Is Mandatory")]
        public string SSN { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Width { get; set; }
        public string Length { get; set; }
        public int? NoOfItems { get; set; }
        public string AmazonOrderNo { get; set; }
    }
}