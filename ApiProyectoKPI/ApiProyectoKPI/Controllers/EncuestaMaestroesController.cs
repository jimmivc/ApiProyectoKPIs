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
    public class EncuestaMaestroesController : ApiController
    {
        private ApiKPIsContext db = new ApiKPIsContext();

        // GET: api/EncuestaMaestroes
        public IQueryable<EncuestaMaestro> GetEncuestaMaestroes()
        {
            return db.EncuestaMaestroes.Include(r => r.RelacionCursos);
        }

        // GET: api/EncuestaMaestroes/5
        [ResponseType(typeof(EncuestaMaestro))]
        public IHttpActionResult GetEncuestaMaestro(int id)
        {
            EncuestaMaestro encuestaMaestro = db.EncuestaMaestroes.Find(id);
            if (encuestaMaestro == null)
            {
                return NotFound();
            }

            return Ok(encuestaMaestro);
        }

        // PUT: api/EncuestaMaestroes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEncuestaMaestro(int id, EncuestaMaestro encuestaMaestro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != encuestaMaestro.EncuestaMaestroID)
            {
                return BadRequest();
            }

            db.Entry(encuestaMaestro).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EncuestaMaestroExists(id))
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

        // POST: api/EncuestaMaestroes
        [ResponseType(typeof(EncuestaMaestro))]
        public IHttpActionResult PostEncuestaMaestro(EncuestaMaestro encuestaMaestro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EncuestaMaestroes.Add(encuestaMaestro);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = encuestaMaestro.EncuestaMaestroID }, encuestaMaestro);
        }

        // DELETE: api/EncuestaMaestroes/5
        [ResponseType(typeof(EncuestaMaestro))]
        public IHttpActionResult DeleteEncuestaMaestro(int id)
        {
            EncuestaMaestro encuestaMaestro = db.EncuestaMaestroes.Find(id);
            if (encuestaMaestro == null)
            {
                return NotFound();
            }

            db.EncuestaMaestroes.Remove(encuestaMaestro);
            db.SaveChanges();

            return Ok(encuestaMaestro);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EncuestaMaestroExists(int id)
        {
            return db.EncuestaMaestroes.Count(e => e.EncuestaMaestroID == id) > 0;
        }
    }
}