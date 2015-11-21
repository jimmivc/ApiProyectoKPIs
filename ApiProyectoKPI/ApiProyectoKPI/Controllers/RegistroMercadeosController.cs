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
    public class RegistroMercadeosController : ApiController
    {
        private ApiKPIsContext db = new ApiKPIsContext();

        // GET: api/RegistroMercadeos
        public IQueryable<RegistroMercadeo> GetRegistrosMercadeo()
        {
            return db.RegistrosMercadeo;
        }

        // GET: api/RegistroMercadeos/5
        [ResponseType(typeof(RegistroMercadeo))]
        public IHttpActionResult GetRegistroMercadeo(int id)
        {
            RegistroMercadeo registroMercadeo = db.RegistrosMercadeo.Find(id);
            if (registroMercadeo == null)
            {
                return NotFound();
            }

            return Ok(registroMercadeo);
        }

        // PUT: api/RegistroMercadeos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRegistroMercadeo(int id, RegistroMercadeo registroMercadeo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != registroMercadeo.RegistroMercadeoID)
            {
                return BadRequest();
            }

            db.Entry(registroMercadeo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistroMercadeoExists(id))
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

        // POST: api/RegistroMercadeos
        [ResponseType(typeof(RegistroMercadeo))]
        public IHttpActionResult PostRegistroMercadeo(RegistroMercadeo registroMercadeo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RegistrosMercadeo.Add(registroMercadeo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = registroMercadeo.RegistroMercadeoID }, registroMercadeo);
        }

        // DELETE: api/RegistroMercadeos/5
        [ResponseType(typeof(RegistroMercadeo))]
        public IHttpActionResult DeleteRegistroMercadeo(int id)
        {
            RegistroMercadeo registroMercadeo = db.RegistrosMercadeo.Find(id);
            if (registroMercadeo == null)
            {
                return NotFound();
            }

            db.RegistrosMercadeo.Remove(registroMercadeo);
            db.SaveChanges();

            return Ok(registroMercadeo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RegistroMercadeoExists(int id)
        {
            return db.RegistrosMercadeo.Count(e => e.RegistroMercadeoID == id) > 0;
        }

        [HttpGet]
        [Route("api/RegistroMercadeos/registrosRol/{idRol}")]
        public IQueryable<RegistroMercadeo> RegistrosMercadeo(int idRol)
        {
            return db.RegistrosMercadeo.Where(b=>b.usuario.Rol.RolID == idRol);
        }
    }
}