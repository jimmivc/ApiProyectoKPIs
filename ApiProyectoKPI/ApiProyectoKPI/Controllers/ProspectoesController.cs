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
using System.Web.Routing;

namespace ApiProyectoKPI.Controllers
{
    public class ProspectoesController : ApiController
    {
        private ApiKPIsContext db = new ApiKPIsContext();

        // GET: api/Prospectoes
        public IQueryable<Prospecto> GetProspectoes()
        {

            return db.Prospectoes.Include (e => e.Evento);

        }
        
        // GET: api/Prospectoes/5
        [ResponseType(typeof(Prospecto))]
        public IHttpActionResult GetProspecto(int id)
        {
            Prospecto prospecto = db.Prospectoes.Where(i => i.ProspectoID == id).
                Include(e => e.Evento).FirstOrDefault();

            if (prospecto == null)
            {
                return NotFound();
            }

            return Ok(prospecto);
        }

        // GET: api/Prospectoes/iden/id
        [Route("api/Prospectoes/iden/{id}")]
        [ResponseType(typeof(Prospecto))]
        public IHttpActionResult GetProspectoIden(int iden)
        {
            Prospecto prospecto = db.Prospectoes.Find();

            if (prospecto == null)
            {
                return NotFound();
            }

            return Ok(prospecto);
        }
        // PUT: api/Prospectoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProspecto(int id, Prospecto prospecto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prospecto.ProspectoID)
            {
                return BadRequest();
            }

            db.Entry(prospecto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProspectoExists(id))
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

        // POST: api/Prospectoes
        [ResponseType(typeof(Prospecto))]
        public IHttpActionResult PostProspecto(Prospecto prospecto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (prospecto == null)
            {
                return NotFound();
            }
            db.Prospectoes.Add(prospecto);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = prospecto.ProspectoID }, prospecto);
        }

        // DELETE: api/Prospectoes/5
        [ResponseType(typeof(Prospecto))]
        public IHttpActionResult DeleteProspecto(int id)
        {
            Prospecto prospecto = db.Prospectoes.Find(id);
            if (prospecto == null)
            {
                return NotFound();
            }

            db.Prospectoes.Remove(prospecto);
            db.SaveChanges();

            return Ok(prospecto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProspectoExists(int id)
        {
            return db.Prospectoes.Count(e => e.ProspectoID == id) > 0;
        }
    }
}