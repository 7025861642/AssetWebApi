//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AssetManmagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchaseTbl
    {
        public int pd_id { get; set; }
        public Nullable<int> pd_order_no { get; set; }
        public Nullable<int> pd_ad_id { get; set; }
        public Nullable<int> pd_atype_id { get; set; }
        public Nullable<decimal> pd_qty { get; set; }
        public Nullable<int> pd_vendor_id { get; set; }
        public DateTime pd_date { get; set; }
        public string pd_datestr
        {
            get
            {
                return pd_date.ToString("yyyy-MM-dd");

            }
        }
        public Nullable<System.DateTime> pd_ddate { get; set; }
        public string pd_ddatestr
        {
            get
            {
                return pd_ddate.ToString();
            }

        }

        public string pd_status { get; set; }
    
        public virtual assetDef assetDef { get; set; }
        public virtual assetType assetType { get; set; }
        public virtual vendorTbl vendorTbl { get; set; }
    }
}
