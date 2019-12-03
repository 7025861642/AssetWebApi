using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AssetManmagementSystem.Models;

namespace AssetManmagementSystem.Controllers
{
    public class PurchaseTblsController : ApiController
    {
        private AssetMSMVCDBEntities7 db = new AssetMSMVCDBEntities7();

        //// GET: api/PurchaseTbls
        //public IQueryable<PurchaseTbl> GetPurchaseTbls()
        //{
        //    return db.PurchaseTbls;
        //}
        public PurchaseTblsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        public List<PurchaseView>GetPurchaseViews()
        {
            db.Configuration.ProxyCreationEnabled = true;

            List<PurchaseTbl> purchaselist = db.PurchaseTbls.ToList();
            List<PurchaseView> viewlist = purchaselist.Select(x => new PurchaseView
            {
                pd_id = x.pd_id,
                pd_order_no = Convert.ToInt32(x.pd_order_no),
                assetname = x.assetDef.ad_name,
                assettype = x.assetType.at_name,
                pd_qty = Convert.ToInt32(x.pd_qty),
                vendorname = x.vendorTbl.vd_name,
                pd_datestr = x.pd_datestr,
                pd_ddatestr =x.pd_ddatestr,
                pd_status = x.pd_status
            }).ToList();
            return viewlist;
        }
        public List<VendorView>GetVendors(int id)
        {
            db.Configuration.ProxyCreationEnabled = true;

            List<vendorTbl> vendorlist = db.vendorTbls.Where(x => x.vd_atype_id == id).ToList();
            List<VendorView> viewlist = vendorlist.Select(x => new VendorView
            {
                vd_id = x.vd_id,
                vd_name = x.vd_name,
                vd_type = x.vd_type,
                vd_atype_id = x.vd_atype_id,
                vd_atype = x.assetType.at_name,
                vd_from = Convert.ToDateTime(x.vd_from),
                vd_to = Convert.ToDateTime(x.vd_to),
                vd_addr = x.vd_addr

            }).ToList();
            return viewlist;

        }
        public List<assetType>GetAssetTypes(string name)
        {
            db.Configuration.ProxyCreationEnabled = true;
            assetDef asset = db.assetDefs.Where(x => x.ad_name == name).FirstOrDefault();
            List<assetType> list = new List<assetType>();
            if(asset!=null)
            {
                list = db.assetTypes.Where(x => x.at_id == asset.ad_type_id).ToList();
            } return list;
        }

        //// GET: api/PurchaseTbls/5
        //[ResponseType(typeof(PurchaseTbl))]
        //public IHttpActionResult GetPurchaseTbl(int id)
        //{
        //    PurchaseTbl purchaseTbl = db.PurchaseTbls.Find(id);
        //    if (purchaseTbl == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(purchaseTbl);
        //}

        // PUT: api/PurchaseTbls/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPurchaseTbl(int id, PurchaseTbl purchaseTbl)
        {
           
            if (id != purchaseTbl.pd_id)
            {
                return BadRequest();
            }

            db.Entry(purchaseTbl).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseTblExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PurchaseTbls
        [ResponseType(typeof(PurchaseTbl))]
        public IHttpActionResult PostPurchaseTbl(PurchaseTbl purchaseTbl)
        {
            purchaseTbl.pd_date = DateTime.Now;

            db.PurchaseTbls.Add(purchaseTbl);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = purchaseTbl.pd_id }, purchaseTbl);
        }

        // DELETE: api/PurchaseTbls/5
        [ResponseType(typeof(PurchaseTbl))]
        public IHttpActionResult DeletePurchaseTbl(int id)
        {
            PurchaseTbl purchaseTbl = db.PurchaseTbls.Find(id);
            if (purchaseTbl == null)
            {
                return NotFound();
            }

            db.PurchaseTbls.Remove(purchaseTbl);
            db.SaveChanges();

            return Ok(purchaseTbl);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PurchaseTblExists(int id)
        {
            return db.PurchaseTbls.Count(e => e.pd_id == id) > 0;
        }
    }
}