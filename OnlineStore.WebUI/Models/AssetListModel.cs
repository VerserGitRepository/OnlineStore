using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.WebUI.Models
{
    public class AssetListModel
    {
       
        public string SSN { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Serial { get; set; }
        public string Colour { get; set; }
        public string Memory { get; set; }
        public string DiskSize { get; set; }
        public string Grade { get; set; }
        public string ItemTypeName { get; set; }
        public string Barcode { get; set; }
        public string ClientRef { get; set; }
        public string ClientPO { get; set; }
        public string ClientAssetTag { get; set; }
        public string Condition { get; set; }
        public string Completeness { get; set; }
        public string Appearance { get; set; }
        public string Services { get; set; }
        public string Operability { get; set; }
        public string ChassisType { get; set; }
        public string IMEI { get; set; }
        public string OpticalDrives { get; set; }
        public string FlattenedPorts { get; set; }
        public string FlattenedAccessories { get; set; }
        public string SoftwareLicense { get; set; }
        public string VideoMemory { get; set; }
        public string CPU { get; set; }
        public string CPUSpeed { get; set; }
        public string ScreenSize { get; set; }
        public string ScreenType { get; set; }
    }
}