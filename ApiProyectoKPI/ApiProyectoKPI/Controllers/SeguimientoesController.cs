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
    public class SeguimientoesController : ApiController
    {
        private ApiKPIsContext db = new ApiKPIsContext();

        // GET: api/Seguimientoes
        public IQueryable<Seguimiento> GetSeguimientoes()
        {
            return db.Seguimientoes;
        }

        // GET: api/Seguimientoes/5
        [ResponseType(typeof(Seguimiento))]
        public IHttpActionResult GetSeguimiento(int id)
        {
            Seguimiento seguimiento = db.Seguimientoes.Where(i => i.SeguimientoID == id).Include(e => e.Usuario).FirstOrDefault();
            //db.Entry(prospecto).Collection(f => f.FormasContactos).Load();
            
            if (seguimiento == null)
            {
                return NotFound();
            }

            return Ok(seguimiento);
        }

        // PUT: api/Seguimientoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSeguimiento(int id, Seguimiento seguimiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != seguimiento.SeguimientoID)
            {
                return BadRequest();
            }

            db.Entry(seguimiento).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeguimientoExists(id))
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

        // POST: api/Seguimientoes
        [ResponseType(typeof(Seguimiento))]
        public IHttpActionResult PostSeguimiento(Seguimiento seguimiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            seguimiento.Prospecto = db.Prospectoes.Find(seguimiento.Prospecto.ProspectoID);
            db.Seguimientoes.Add(seguimiento);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = seguimiento.SeguimientoID }, seguimiento);
        }

        // DELETE: api/Seguimientoes/5
        [ResponseType(typeof(Seguimiento))]
        public IHttpActionResult DeleteSeguimiento(int id)
        {
            Seguimiento seguimiento = db.Seguimientoes.Find(id);
            if (seguimiento == null)
            {
                return NotFound();
            }

            db.Seguimientoes.Remove(seguimiento);
            db.SaveChanges();

            return Ok(seguimiento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SeguimientoExists(int id)
        {
            return db.Seguimientoes.Count(e => e.SeguimientoID == id) > 0;
        }
    }
}