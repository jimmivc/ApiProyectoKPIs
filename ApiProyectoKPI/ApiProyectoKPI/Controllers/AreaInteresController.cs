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
    public class AreaInteresController : ApiController
    {
        private ApiKPIsContext db = new ApiKPIsContext();

        // GET: api/AreaInteres
        public IQueryable<AreaInteres> GetAreaInteres()
        {
            return db.AreaInteres;
        }

        // GET: api/AreaInteres/5
        [ResponseType(typeof(AreaInteres))]
        public IHttpActionResult GetAreaInteres(int id)
        {
            AreaInteres areaInteres = db.AreaInteres.Find(id);
            if (areaInteres == null)
            {
                return NotFound();
            }

            return Ok(areaInteres);
        }

        // PUT: api/AreaInteres/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAreaInteres(int id, AreaInteres areaInteres)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != areaInteres.AreaInteresID)
            {
                return BadRequest();
            }

            db.Entry(areaInteres).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AreaInteresExists(id))
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

        // POST: api/AreaInteres
        [ResponseType(typeof(AreaInteres))]
        public IHttpActionResult PostAreaInteres(AreaInteres areaInteres)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AreaInteres.Add(areaInteres);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = areaInteres.AreaInteresID }, areaInteres);
        }

        // DELETE: api/AreaInteres/5
        [ResponseType(typeof(AreaInteres))]
        public IHttpActionResult DeleteAreaInteres(int id)
        {
            AreaInteres areaInteres = db.AreaInteres.Find(id);
            if (areaInteres == null)
            {
                return NotFound();
            }

            db.AreaInteres.Remove(areaInteres);
            db.SaveChanges();

            return Ok(areaInteres);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AreaInteresExists(int id)
        {
            return db.AreaInteres.Count(e => e.AreaInteresID == id) > 0;
        }
    }
}