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
    public class AssetDefinitionController : ApiController
    {
        private AssetMSMVCDBEntities7 db = new AssetMSMVCDBEntities7();

        // GET: api/assetDefs
        //public IQueryable<assetDef> GetassetDefs()
        //{
        //    return db.assetDefs;
        //}

        // GET: api/assetDefs/5
        [ResponseType(typeof(assetDef))]
        public IHttpActionResult GetassetDef(int id)
        {
            assetDef assetDef = db.assetDefs.Find(id);
            if (assetDef == null)
            {
                return NotFound();
            }

            return Ok(assetDef);
        }

        // PUT: api/assetDefs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutassetDef(int id, assetDef assetDef)
        {
           

            if (id != assetDef.ad_id)
            {
                return BadRequest();
            }

            db.Entry(assetDef).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!assetDefExists(id))
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

        // POST: api/assetDefs
        [ResponseType(typeof(assetDef))]
        public int PostassetDef(assetDef assetDef)
        {

            //db.assetDefs.Add(assetDef);
            //db.SaveChanges();

            //return CreatedAtRoute("DefaultApi", new { id = assetDef.ad_id }, assetDef);
            assetDef asset = new assetDef();
            asset = db.assetDefs.Where(x => x.ad_name == assetDef.ad_name && x.ad_type_id == assetDef.ad_type_id && x.ad_class == assetDef.ad_class).FirstOrDefault();
            if(asset==null)
            {
                db.assetDefs.Add(assetDef);
                db.SaveChanges();
                return 0;

            }
            else
            {
                return 1;
            }
        }

        // DELETE: api/assetDefs/5
        [ResponseType(typeof(assetDef))]
        public IHttpActionResult DeleteassetDef(int id)
        {
            assetDef assetDef = db.assetDefs.Find(id);
            if (assetDef == null)
            {
                return NotFound();
            }

            db.assetDefs.Remove(assetDef);
            db.SaveChanges();

            return Ok(assetDef);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool assetDefExists(int id)
        {
            return db.assetDefs.Count(e => e.ad_id == id) > 0;
        }
        public List<AssetViewModel> GetAssetViewModels()
        {
            db.Configuration.ProxyCreationEnabled = true;
            {
                List<assetDef> assetList = db.assetDefs.ToList();
                List<AssetViewModel> avList = assetList.Select(x => new AssetViewModel
                {
                    ad_id = x.ad_id,
                    ad_name = x.ad_name,
                    ad_type = x.assetType.at_name,
                    ad_class = x.ad_class
                }).ToList();
                return avList;
            }
        }

   }
}