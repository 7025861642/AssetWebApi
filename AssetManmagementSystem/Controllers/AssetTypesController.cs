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
    public class AssetTypesController : ApiController
    {
        private AssetMSMVCDBEntities7 db = new AssetMSMVCDBEntities7();

        // GET: api/AssetTypes
        public IQueryable<assetType> GetassetTypes()
        {
            return db.assetTypes;
        }

        // GET: api/AssetTypes/5
        [ResponseType(typeof(assetType))]
        public IHttpActionResult GetassetType(int id)
        {
            assetType assetType = db.assetTypes.Find(id);
            if (assetType == null)
            {
                return NotFound();
            }

            return Ok(assetType);
        }

        // PUT: api/AssetTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutassetType(int id, assetType assetType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != assetType.at_id)
            {
                return BadRequest();
            }

            db.Entry(assetType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!assetTypeExists(id))
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

        // POST: api/AssetTypes
        [ResponseType(typeof(assetType))]
        public IHttpActionResult PostassetType(assetType assetType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.assetTypes.Add(assetType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = assetType.at_id }, assetType);
        }
        public AssetTypesController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        // DELETE: api/AssetTypes/5
        [ResponseType(typeof(assetType))]
        public IHttpActionResult DeleteassetType(int id)
        {
            assetType assetType = db.assetTypes.Find(id);
            if (assetType == null)
            {
                return NotFound();
            }

            db.assetTypes.Remove(assetType);
            db.SaveChanges();

            return Ok(assetType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool assetTypeExists(int id)
        {
            return db.assetTypes.Count(e => e.at_id == id) > 0;
        }

    }
}
