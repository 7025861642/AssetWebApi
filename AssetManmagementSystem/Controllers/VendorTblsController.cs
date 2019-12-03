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
    public class VendorTblsController : ApiController
    {
        private AssetMSMVCDBEntities7 db = new AssetMSMVCDBEntities7();

        //// GET: api/VendorTbls
        //public IQueryable<vendorTbl> GetvendorTbls()
        //{
        //    return db.vendorTbls;
        //}

        // GET: api/VendorTbls/5
        [ResponseType(typeof(vendorTbl))]
        public IHttpActionResult GetvendorTbl(int id)
        {
            vendorTbl vendorTbl = db.vendorTbls.Find(id);
            if (vendorTbl == null)
            {
                return NotFound();
            }

            return Ok(vendorTbl);
        }
        public VendorTblsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        public List<VendorView> GetAssetViewModels()
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<vendorTbl> vendorList = db.vendorTbls.ToList();
            List<VendorView> avList = vendorList.Select(x => new VendorView
            {
                vd_id = x.vd_id,
                vd_name=x.vd_name,
                vd_type=x.vd_type,
                vd_atype=x.assetType.at_name,
                vd_from=Convert.ToDateTime(x.vd_from),
                vd_to=Convert.ToDateTime(x.vd_to),
                vd_addr=x.vd_addr

        }).ToList();
                return avList;
        }

    // PUT: api/VendorTbls/5
    [ResponseType(typeof(void))]
        public IHttpActionResult PutvendorTbl(int id, vendorTbl vendorTbl)
        {
           

            if (id != vendorTbl.vd_id)
            {
                return BadRequest();
            }

            db.Entry(vendorTbl).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!vendorTblExists(id))
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

        // POST: api/VendorTbls
        [ResponseType(typeof(vendorTbl))]
        public int PostvendorTbl(vendorTbl vendorTbl)
        {
            vendorTbl vendor = new vendorTbl();
            vendor = db.vendorTbls.Where(x => x.vd_name == vendorTbl.vd_name).FirstOrDefault();
            if(vendor==null)
            {
                db.vendorTbls.Add(vendorTbl);
                db.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
           

           
        }

        // DELETE: api/VendorTbls/5
        [ResponseType(typeof(vendorTbl))]
        public IHttpActionResult DeletevendorTbl(int id)
        {
            vendorTbl vendorTbl = db.vendorTbls.Find(id);
            if (vendorTbl == null)
            {
                return NotFound();
            }

            db.vendorTbls.Remove(vendorTbl);
            db.SaveChanges();

            return Ok(vendorTbl);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool vendorTblExists(int id)
        {
            return db.vendorTbls.Count(e => e.vd_id == id) > 0;
        }
    }
}