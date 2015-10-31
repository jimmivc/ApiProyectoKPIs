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
    public class TipoFormaContactoesController : ApiController
    {
        private ApiKPIsContext db = new ApiKPIsContext();

        // GET: api/TipoFormaContactoes
        public IQueryable<TipoFormaContacto> GetTipoFormaContactoes()
        {
            return db.TipoFormaContactoes;
        }

        // GET: api/TipoFormaContactoes/5
        [ResponseType(typeof(TipoFormaContacto))]
        public IHttpActionResult GetTipoFormaContacto(int id)
        {
            TipoFormaContacto tipoFormaContacto = db.TipoFormaContactoes.Find(id);
            if (tipoFormaContacto == null)
            {
                return NotFound();
            }

            return Ok(tipoFormaContacto);
        }

        // PUT: api/TipoFormaContactoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTipoFormaContacto(int id, TipoFormaContacto tipoFormaContacto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoFormaContacto.TipoFormaContactoID)
            {
                return BadRequest();
            }

            db.Entry(tipoFormaContacto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoFormaContactoExists(id))
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

        // POST: api/TipoFormaContactoes
        [ResponseType(typeof(TipoFormaContacto))]
        public IHttpActionResult PostTipoFormaContacto(TipoFormaContacto tipoFormaContacto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipoFormaContactoes.Add(tipoFormaContacto);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tipoFormaContacto.TipoFormaContactoID }, tipoFormaContacto);
        }

        // DELETE: api/TipoFormaContactoes/5
        [ResponseType(typeof(TipoFormaContacto))]
        public IHttpActionResult DeleteTipoFormaContacto(int id)
        {
            TipoFormaContacto tipoFormaContacto = db.TipoFormaContactoes.Find(id);
            if (tipoFormaContacto == null)
            {
                return NotFound();
            }

            db.TipoFormaContactoes.Remove(tipoFormaContacto);
            db.SaveChanges();

            return Ok(tipoFormaContacto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoFormaContactoExists(int id)
        {
            return db.TipoFormaContactoes.Count(e => e.TipoFormaContactoID == id) > 0;
        }
    }
}