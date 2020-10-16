using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.WebUI.Models
{
    public class OnlineSaleProduct
    {
        public OnlineSaleProduct()
        {
            Images = new List<string>();
        }
        public int Id { get; set; }
        [RequiredIf("IsUpdateProduct", false, "Model is mandatory")]
        public byte[] ProductImage { get; set; }
        [Required(ErrorMessage = "ProductName Is Mandatory")]
        public string ProductName { get; set; }
        public string Description { get; set; }
        public bool IsSKU { get; set; }
        [Required(ErrorMessage = "ItemType Is Mandatory")]
        public int? ItemType_ID { get; set; }
        [Required(ErrorMessage = "Brand Is Mandatory")]
        public int? Brand_ID { get; set; }
        [RequiredIf("IsUpdateProduct",false, "Model is mandatory")]
        public int? Model_ID { get; set; }
        public int PurchasedQty { get; set; }
        public decimal PriceExGST { get; set; }
        public decimal GSTAmount { get; set; }
        [Required(ErrorMessage = "Price Is Mandatory")]
        public decimal PriceIncGST { get; set; }
        [Required(ErrorMessage = "Quantity Is Mandatory")]
        public int QtyAvailable { get; set; }
        public string CreatedBy { get; set; }
        public string ItemTypeName { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public SelectList ItemTypes { get; set; }
        public SelectList Makes { get; set; }
        public SelectList Colours { get; set; }
        public SelectList Sizes { get; set; }
        public string SKU { get; set; }
        public SelectList Models { get; set; }
        public List<string> Images { get; set; }
        [RequiredIf("IsUpdateProduct",false, "File is mandatory")]
        [Newtonsoft.Json.JsonIgnore]
        public HttpPostedFileBase[] files { get; set; }
        public bool IsProductActive { get; set; } = true;
        public bool IsMainPageProduct { get; set; }= true;
        public bool IsUpdateProduct { get; set; }
        public bool IsViewTypeGrid { get; set; }

    }
    public class RequiredIfAttribute : ValidationAttribute
    {
        private string PropertyName { get; set; }
        private string ErrorMessage { get; set; }
        private Object DesiredValue { get; set; }

        public RequiredIfAttribute(String propertyName, Object desiredvalue, String errormessage)
        {
            this.PropertyName = propertyName;
            this.DesiredValue = desiredvalue;
            this.ErrorMessage = errormessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            Object instance = context.ObjectInstance;
            Type type = instance.GetType();
            Object proprtyvalue = type.GetProperty(PropertyName).GetValue(instance, null);
            if (proprtyvalue.ToString() == DesiredValue.ToString() )
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
        public System.Collections.Generic.IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());
            rule.ValidationParameters.Add("string", ErrorMessage);
            rule.ValidationType = "required";
            yield return rule;
        }
    }
}
