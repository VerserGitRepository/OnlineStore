using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.WebUI.Models
{
    public class ReturnValidationModel
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}