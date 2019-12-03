
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
    public class Asset_MasterOrderController : ApiController
    {
        private AssetMSMVCDBEntities7 db = new AssetMSMVCDBEntities7();

        Asset_MasterOrderController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Asset_MasterOrder
        //public IQueryable<asset_master> Getasset_master()
        //{
        //    return db.asset_master;
        //}

        // GET: api/Asset_MasterOrder/5
        // GET: api/AssetMasterOrderView
        public List<PurchaseView> GetAsset_master()
        {
            db.Configuration.ProxyCreationEnabled = true;

            List<PurchaseTbl> pList = db.PurchaseTbls.Where(x => x.pd_status == "Consignment Received").ToList();
            List<PurchaseView> pvList = pList.Select(x => new PurchaseView
            {
                pd_id = x.pd_id,
                pd_order_no = Convert.ToInt32(x.pd_order_no),
                pd_ad_id = x.assetDef.ad_id,
                assetname = x.assetDef.ad_name,
                pd_datestr = x.pd_datestr,
                pd_ddatestr = x.pd_ddatestr,
                pd_qty = Convert.ToInt32(x.pd_qty),
                pd_status = x.pd_status,
                pd_atype_id= x.assetType.at_id,
                assettype = x.assetType.at_name,
                pd_vendor_id = x.pd_vendor_id,
                vendorname = x.vendorTbl.vd_name



            }).ToList();

            return pvList;
        }

        // GET: api/AssetMasterOrderView/5
        [ResponseType(typeof(Asset_master))]
        public PurchaseView GetAsset_master(int ordno)
        {
            db.Configuration.ProxyCreationEnabled = true;
            PurchaseTbl x = db.PurchaseTbls.Where(y => y.pd_order_no == ordno).FirstOrDefault();
            PurchaseView pvModel = new PurchaseView();

            if (x == null)
            {

            }

            else
            {
                pvModel.pd_id = x.pd_id;
                pvModel.pd_order_no = Convert.ToInt32(x.pd_order_no);
                pvModel.pd_ad_id = x.assetDef.ad_id;
                pvModel.assetname = x.assetDef.ad_name;
                pvModel.pd_datestr = x.pd_datestr;
                pvModel.pd_ddatestr = x.pd_ddatestr;
                pvModel.pd_qty = Convert.ToInt32(x.pd_qty);
                pvModel.pd_status = x.pd_status;
                pvModel.pd_atype_id = x.assetType.at_id;
                pvModel.assettype = x.assetType.at_name;
                pvModel.pd_vendor_id = x.pd_vendor_id;
                pvModel.vendorname = x.vendorTbl.vd_name;
            }

            return pvModel;
        }

        // PUT: api/Asset_MasterOrder/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putasset_master(int id, Asset_master asset_master)
        {


            if (id != asset_master.am_id)
            {
                return BadRequest();
            }

            db.Entry(asset_master).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!asset_masterExists(id))
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

        // POST: api/Asset_MasterOrder
        [ResponseType(typeof(Asset_master))]
        public IHttpActionResult Postasset_master(Asset_master asset_master)
        {


            db.Asset_master.Add(asset_master);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = asset_master.am_id }, asset_master);
        }

        // DELETE: api/Asset_MasterOrder/5
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