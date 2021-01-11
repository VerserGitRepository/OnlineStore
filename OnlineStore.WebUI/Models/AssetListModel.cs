﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.WebUI.Models
{
    public class AssetListModel
    {
        public int Id { get; set; }
        public string SSN { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Serial { get; set; }
        public string Colour { get; set; }
        public string Memory { get; set; }
        public string Grade { get; set; }
        public string ItemTypeName { get; set; }
    }
}