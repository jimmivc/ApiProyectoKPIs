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
    public class GrupoEmpresarialsController : ApiController
    {
        private ApiKPIsContext db = new ApiKPIsContext();

        // GET: api/GrupoEmpresarials
        public IQueryable<GrupoEmpresarial> GetGrupoEmpresarials()
        {
            return db.GrupoEmpresarials;
        }

        // GET: api/GrupoEmpresarials/5
        [ResponseType(typeof(GrupoEmpresarial))]
        public IHttpActionResult GetGrupoEmpresarial(int id)
        {
            GrupoEmpresarial grupoEmpresarial = db.GrupoEmpresarials.Find(id);
            if (grupoEmpresarial == null)
            {
                return NotFound();
            }

            return Ok(grupoEmpresarial);
        }

        // PUT: api/GrupoEmpresarials/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGrupoEmpresarial(int id, GrupoEmpresarial grupoEmpresarial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != grupoEmpresarial.GrupoEmpresarialID)
            {
                return BadRequest();
            }

            db.Entry(grupoEmpresarial).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GrupoEmpresarialExists(id))
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

        // POST: api/GrupoEmpresarials
        [ResponseType(typeof(GrupoEmpresarial))]
        public IHttpActionResult PostGrupoEmpresarial(GrupoEmpresarial grupoEmpresarial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GrupoEmpresarials.Add(grupoEmpresarial);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = grupoEmpresarial.GrupoEmpresarialID }, grupoEmpresarial);
        }

        // DELETE: api/GrupoEmpresarials/5
        [ResponseType(typeof(GrupoEmpresarial))]
        public IHttpActionResult DeleteGrupoEmpresarial(int id)
        {
            GrupoEmpresarial grupoEmpresarial = db.GrupoEmpresarials.Find(id);
            if (grupoEmpresarial == null)
            {
                return NotFound();
            }

            db.GrupoEmpresarials.Remove(grupoEmpresarial);
            db.SaveChanges();

            return Ok(grupoEmpresarial);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GrupoEmpresarialExists(int id)
        {
            return db.GrupoEmpresarials.Count(e => e.GrupoEmpresarialID == id) > 0;
        }
    }
}