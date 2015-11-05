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
using ApiProyectoKPI.Controllers.DataBaseContext;
using ApiProyectoKPI.Models;

namespace ApiProyectoKPI.Controllers
{
    public class ParametroKPIsController : ApiController
    {
        private ApiKPIsContext db = new ApiKPIsContext();

        // GET: api/ParametroKPIs
        public IQueryable<ParametroKPI> GetParametroKPIs()
        {
            return db.ParametroKPIs;
        }

        // GET: api/ParametroKPIs/5
        [ResponseType(typeof(ParametroKPI))]
        public IHttpActionResult GetParametroKPI(int id)
        {
            ParametroKPI parametroKPI = db.ParametroKPIs.Find(id);
            if (parametroKPI == null)
            {
                return NotFound();
            }

            return Ok(parametroKPI);
        }

        // PUT: api/ParametroKPIs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParametroKPI(int id, ParametroKPI parametroKPI)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parametroKPI.ParametroKPIID)
            {
                return BadRequest();
            }

            db.Entry(parametroKPI).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParametroKPIExists(id))
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

        // POST: api/ParametroKPIs
        [ResponseType(typeof(ParametroKPI))]
        public IHttpActionResult PostParametroKPI(ParametroKPI parametroKPI)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ParametroKPIs.Add(parametroKPI);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = parametroKPI.ParametroKPIID }, parametroKPI);
        }

        // DELETE: api/ParametroKPIs/5
        [ResponseType(typeof(ParametroKPI))]
        public IHttpActionResult DeleteParametroKPI(int id)
        {
            ParametroKPI parametroKPI = db.ParametroKPIs.Find(id);
            if (parametroKPI == null)
            {
                return NotFound();
            }

            db.ParametroKPIs.Remove(parametroKPI);
            db.SaveChanges();

            return Ok(parametroKPI);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParametroKPIExists(int id)
        {
            return db.ParametroKPIs.Count(e => e.ParametroKPIID == id) > 0;
        }
    }
}