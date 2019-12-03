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
    public class PurchaseEditController : ApiController
    {
        private AssetMSMVCDBEntities7 db = new AssetMSMVCDBEntities7();

        // GET: api/PurchaseEdit
        public IQueryable<PurchaseTbl> GetPurchaseTbls()
        {
            return db.PurchaseTbls;
        }
        public PurchaseEditController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        //// GET: api/PurchaseEdit/5
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
        public PurchaseView GetPurchaseOrder(int id)
        {
            db.Configuration.ProxyCreationEnabled = true;
            PurchaseTbl order = db.PurchaseTbls.Where(x => x.pd_id == id).FirstOrDefault();
            PurchaseView model = new PurchaseView();
            model.pd_id = order.pd_id;
            model.pd_order_no =Convert.ToInt32( order.pd_order_no);
            model.assetname = order.assetDef.ad_name;
            model.assettype = order.assetType.at_name;
            model.pd_ad_id = order.pd_ad_id;
            model.pd_atype_id = order.pd_atype_id;
            model.pd_vendor_id = order.pd_vendor_id;
            model.vendorname = order.vendorTbl.vd_name;
            model.pd_datestr = order.pd_datestr;
            model.pd_ddatestr = order.pd_ddatestr;
            model.pd_qty = Convert.ToInt32(order.pd_qty);
            model.pd_status = order.pd_status;



            return model;
        }

        // PUT: api/PurchaseEdit/5
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

        // POST: api/PurchaseEdit
        [ResponseType(typeof(PurchaseTbl))]
        public IHttpActionResult PostPurchaseTbl(PurchaseTbl purchaseTbl)
        {
            

            db.PurchaseTbls.Add(purchaseTbl);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = purchaseTbl.pd_id }, purchaseTbl);
        }

        // DELETE: api/PurchaseEdit/5
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