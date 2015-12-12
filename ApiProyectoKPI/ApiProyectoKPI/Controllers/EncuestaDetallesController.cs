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
    public class EncuestaDetallesController : ApiController
    {
        private ApiKPIsContext db = new ApiKPIsContext();

        // GET: api/EncuestaDetalles
        public IQueryable<EncuestaDetalle> GetEncuestaDetalles()
        {
            return db.EncuestaDetalles;
        }

        // GET: api/EncuestaDetalles/5
        [ResponseType(typeof(EncuestaDetalle))]
        public IHttpActionResult GetEncuestaDetalle(int id)
        {
            EncuestaDetalle encuestaDetalle = db.EncuestaDetalles.Find(id);
            if (encuestaDetalle == null)
            {
                return NotFound();
            }

            return Ok(encuestaDetalle);
        }

        // PUT: api/EncuestaDetalles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEncuestaDetalle(int id, EncuestaDetalle encuestaDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != encuestaDetalle.EncuestaDetalleID)
            {
                return BadRequest();
            }

            db.Entry(encuestaDetalle).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EncuestaDetalleExists(id))
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

        // POST: api/EncuestaDetalles
        [ResponseType(typeof(EncuestaDetalle))]
        public IHttpActionResult PostEncuestaDetalle(EncuestaDetalle encuestaDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EncuestaDetalles.Add(encuestaDetalle);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = encuestaDetalle.EncuestaDetalleID }, encuestaDetalle);
        }

        // DELETE: api/EncuestaDetalles/5
        [ResponseType(typeof(EncuestaDetalle))]
        public IHttpActionResult DeleteEncuestaDetalle(int id)
        {
            EncuestaDetalle encuestaDetalle = db.EncuestaDetalles.Find(id);
            if (encuestaDetalle == null)
            {
                return NotFound();
            }

            db.EncuestaDetalles.Remove(encuestaDetalle);
            db.SaveChanges();

            return Ok(encuestaDetalle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EncuestaDetalleExists(int id)
        {
            return db.EncuestaDetalles.Count(e => e.EncuestaDetalleID == id) > 0;
        }
    }
}