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
    public class PermisoesController : ApiController
    {
        private ApiKPIsContext db = new ApiKPIsContext();

        // GET: api/Permisoes
        public IQueryable<Permiso> GetPermisoes()
        {
            return db.Permisoes;
        }

        // GET: api/Permisoes/5
        [ResponseType(typeof(Permiso))]
        public IHttpActionResult GetPermiso(int id)
        {
            Permiso permiso = db.Permisoes.Find(id);
            if (permiso == null)
            {
                return NotFound();
            }

            return Ok(permiso);
        }

        // PUT: api/Permisoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPermiso(int id, Permiso permiso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != permiso.PermisoID)
            {
                return BadRequest();
            }

            db.Entry(permiso).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermisoExists(id))
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

        // POST: api/Permisoes
        [ResponseType(typeof(Permiso))]
        public IHttpActionResult PostPermiso(Permiso permiso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Permisoes.Add(permiso);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = permiso.PermisoID }, permiso);
        }

        // DELETE: api/Permisoes/5
        [ResponseType(typeof(Permiso))]
        public IHttpActionResult DeletePermiso(int id)
        {
            Permiso permiso = db.Permisoes.Find(id);
            if (permiso == null)
            {
                return NotFound();
            }

            db.Permisoes.Remove(permiso);
            db.SaveChanges();

            return Ok(permiso);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PermisoExists(int id)
        {
            return db.Permisoes.Count(e => e.PermisoID == id) > 0;
        }

    }
}