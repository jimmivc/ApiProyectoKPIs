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
    public class TipoCursoesController : ApiController
    {
        private ApiKPIsContext db = new ApiKPIsContext();

        // GET: api/TipoCursoes
        public IQueryable<TipoCurso> GetTipoCursoes()
        {
            return db.TipoCursoes;
        }

        // GET: api/TipoCursoes/5
        [ResponseType(typeof(TipoCurso))]
        public IHttpActionResult GetTipoCurso(int id)
        {
            TipoCurso tipoCurso = db.TipoCursoes.Find(id);
            if (tipoCurso == null)
            {
                return NotFound();
            }

            return Ok(tipoCurso);
        }

        // PUT: api/TipoCursoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTipoCurso(int id, TipoCurso tipoCurso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoCurso.TipoCursoID)
            {
                return BadRequest();
            }

            db.Entry(tipoCurso).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoCursoExists(id))
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

        // POST: api/TipoCursoes
        [ResponseType(typeof(TipoCurso))]
        public IHttpActionResult PostTipoCurso(TipoCurso tipoCurso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipoCursoes.Add(tipoCurso);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tipoCurso.TipoCursoID }, tipoCurso);
        }

        // DELETE: api/TipoCursoes/5
        [ResponseType(typeof(TipoCurso))]
        public IHttpActionResult DeleteTipoCurso(int id)
        {
            TipoCurso tipoCurso = db.TipoCursoes.Find(id);
            if (tipoCurso == null)
            {
                return NotFound();
            }

            db.TipoCursoes.Remove(tipoCurso);
            db.SaveChanges();

            return Ok(tipoCurso);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoCursoExists(int id)
        {
            return db.TipoCursoes.Count(e => e.TipoCursoID == id) > 0;
        }
    }
}