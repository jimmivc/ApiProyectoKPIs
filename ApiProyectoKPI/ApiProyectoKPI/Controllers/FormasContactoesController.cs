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
    public class FormasContactoesController : ApiController
    {
        private ApiKPIsContext db = new ApiKPIsContext();

        // GET: api/FormasContactoes
        public IQueryable<FormasContacto> GetFormasContactoes()
        {
            return db.FormasContactoes;
        }

        // GET: api/FormasContactoes/5
        [ResponseType(typeof(FormasContacto))]
        public IHttpActionResult GetFormasContacto(int id)
        {
            FormasContacto formasContacto = db.FormasContactoes.Find(id);
            if (formasContacto == null)
            {
                return NotFound();
            }

            return Ok(formasContacto);
        }

        // PUT: api/FormasContactoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFormasContacto(int id, FormasContacto formasContacto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != formasContacto.FormasContactoID)
            {
                return BadRequest();
            }

            db.Entry(formasContacto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormasContactoExists(id))
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

        // POST: api/FormasContactoes
        [ResponseType(typeof(FormasContacto))]
        public IHttpActionResult PostFormasContacto(FormasContacto formasContacto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FormasContactoes.Add(formasContacto);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = formasContacto.FormasContactoID }, formasContacto);
        }

        // DELETE: api/FormasContactoes/5
        [ResponseType(typeof(FormasContacto))]
        public IHttpActionResult DeleteFormasContacto(int id)
        {
            FormasContacto formasContacto = db.FormasContactoes.Find(id);
            if (formasContacto == null)
            {
                return NotFound();
            }

            db.FormasContactoes.Remove(formasContacto);
            db.SaveChanges();

            return Ok(formasContacto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FormasContactoExists(int id)
        {
            return db.FormasContactoes.Count(e => e.FormasContactoID == id) > 0;
        }
    }
}