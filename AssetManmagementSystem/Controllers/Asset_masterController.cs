using AssetManmagementSystem.Models;
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


namespace AssetManagementAngular.Controllers
{
    public class Asset_MasterController : ApiController
    {
        private AssetMSMVCDBEntities7 db = new AssetMSMVCDBEntities7();
        static decimal count;

        Asset_MasterController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        //// GET: api/Asset_Master
        //public IQueryable<asset_master> Getasset_master()
        //{
        //    return db.asset_master;
        //}
        // GET: api/AssetMaster
        public List<Assetmasterview> GetAsset_master()
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<Asset_master> amList = db.Asset_master.ToList();
            List<Assetmasterview> amvList = amList.Select(x => new Assetmasterview
            {
                am_id = x.am_id,
                am_ad_id = x.am_ad_id,
                am_ad_name = x.assetDef.ad_name,
                am_atype_id = x.am_atype_id,
                am_atype_name = x.assetType.at_name,
                am_from = x.am_from,
                am_to = x.am_to,
                am_make_id = x.am_make_id,
                am_make_name = x.vendorTbl.vd_name,
                am_model = x.am_model,
                am_myyear = Convert.ToString(x.am_myyear),
                am_pdate = Convert.ToDateTime(x.am_pdate),
                am_snumber = x.am_snumber,
                am_warranty = x.am_warranty

            }).ToList();
            return amvList;
        }

        // GET: api/Asset_Master/5
        [ResponseType(typeof(Asset_master))]
        public IHttpActionResult Getasset_master(int id)
        {
            Asset_master asset_master = db.Asset_master.Find(id);
            if (asset_master == null)
            {
                return NotFound();
            }

            return Ok(asset_master);
        }

        // PUT: api/AssetMaster/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPurchase_order(int id, PurchaseTbl purchase_order)
        {



            count = Convert.ToDecimal(purchase_order.pd_qty);
            db.Entry(purchase_order).State = EntityState.Modified;
            db.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/AssetMaster
        [ResponseType(typeof(Asset_master))]
        public IHttpActionResult PostAsset_master(Asset_master asset_master)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int i = 0; i < count; i++)
            {
                int min = 1000;
                int max = 9999;
                Random rdm = new Random();
                int id = rdm.Next(min, max);
                asset_master.am_snumber = id.ToString();
                db.Asset_master.Add(asset_master);
                db.SaveChanges();
            }



            return CreatedAtRoute("DefaultApi", new { id = asset_master.am_id }, asset_master);
        }

        // DELETE: api/Asset_Master/5
        [ResponseType(typeof(Asset_master))]
        public IHttpActionResult Deleteasset_master(int id)
        {
            Asset_master asset_master = db.Asset_master.Find(id);
            if (asset_master == null)
            {
                return NotFound();
            }

            db.Asset_master.Remove(asset_master);
            db.SaveChanges();

            return Ok(asset_master);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool asset_masterExists(int id)
        {
            return db.Asset_master.Count(e => e.am_id == id) > 0;
        }
    }
}