using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetManmagementSystem.Models
{
    public class PurchaseView
    {
        public int pd_id { get; set; }
        public int pd_order_no { get; set; }
        public Nullable<int> pd_ad_id { get; set; }
        public string assetname { get; set; }
        public Nullable<int> pd_atype_id { get; set; }
        public string assettype { get; set; }
        public decimal pd_qty { get; set; }
        public Nullable<int> pd_vendor_id { get; set; }
        public string vendorname { get; set; }
        public string pd_datestr { get; set; }
       
        public string pd_ddatestr { get; set; }
      
        public string pd_status { get; set; }

    }
}