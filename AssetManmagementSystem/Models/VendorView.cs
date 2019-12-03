using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetManmagementSystem.Models
{
    public class VendorView
    {
        internal object vd_atype_id;

        public int vd_id { get; set; }
        public string vd_name { get; set; }
        public string vd_type { get; set; }
        public string vd_atype { get; set; }
        public DateTime vd_from { get; set; }
        public DateTime vd_to { get; set; }
        public string vd_addr { get; set; }

        public string vd_fromstr
        {

            get
            {
                return vd_from.ToString("yyyy/MM/dd");
            }
        }
        public string vd_tostr
        {
            get
            {
                return vd_to.ToString("yyyy/MM/dd");
            }
        }
    }
}