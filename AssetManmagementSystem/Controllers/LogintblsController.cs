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
    public class LogintblsController : ApiController
    {
        private AssetMSMVCDBEntities7 db = new AssetMSMVCDBEntities7();

        // GET: api/logintbls
        public IQueryable<logintbl> Getlogintbls()
        {
            return db.logintbls;
        }

        public List<logintbl> Getlogintbls(string username, string password)
        {
            return db.logintbls.Where(x=>x.username==username&&x.password==password).ToList();
        }

        // GET: api/logintbls/5
        [ResponseType(typeof(logintbl))]
        public IHttpActionResult Getlogintbl(int id)
        {
            logintbl logintbl = db.logintbls.Find(id);
            if (logintbl == null)
            {
                return NotFound();
            }

            return Ok(logintbl);
        }

        // PUT: api/logintbls/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putlogintbl(int id, logintbl logintbl)
        {
           

            if (id != logintbl.l_id)
            {
                return BadRequest();
            }

            db.Entry(logintbl).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!logintblExists(id))
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

        // POST: api/logintbls
        [ResponseType(typeof(logintbl))]
        public IHttpActionResult Postlogintbl(logintbl logintbl)
        {
          

            db.logintbls.Add(logintbl);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = logintbl.l_id }, logintbl);
        }

        // DELETE: api/logintbls/5
        [ResponseType(typeof(logintbl))]
        public IHttpActionResult Deletelogintbl(int id)
        {
            logintbl logintbl = db.logintbls.Find(id);
            if (logintbl == null)
            {
                return NotFound();
            }

            db.logintbls.Remove(logintbl);
            db.SaveChanges();

            return Ok(logintbl);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool logintblExists(int id)
        {
            return db.logintbls.Count(e => e.l_id == id) > 0;
        }
    }
}