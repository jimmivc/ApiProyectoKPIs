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
    public class DetalleFormulasController : ApiController
    {
        private ApiKPIsContext db = new ApiKPIsContext();

        // GET: api/DetalleFormulas
        public IQueryable<DetalleFormula> GetDetalleFormulas()
        {
            return db.DetalleFormulas;
        }

        // GET: api/DetalleFormulas/5
        [ResponseType(typeof(DetalleFormula))]
        public IHttpActionResult GetDetalleFormula(int id)
        {
            DetalleFormula detalleFormula = db.DetalleFormulas.Find(id);
            if (detalleFormula == null)
            {
                return NotFound();
            }

            return Ok(detalleFormula);
        }

        // PUT: api/DetalleFormulas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDetalleFormula(int id, DetalleFormula detalleFormula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != detalleFormula.DetalleFormulaID)
            {
                return BadRequest();
            }

            db.Entry(detalleFormula).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleFormulaExists(id))
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

        // POST: api/DetalleFormulas
        [ResponseType(typeof(DetalleFormula))]
        public IHttpActionResult PostDetalleFormula(DetalleFormula detalleFormula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DetalleFormulas.Add(detalleFormula);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = detalleFormula.DetalleFormulaID }, detalleFormula);
        }

        // DELETE: api/DetalleFormulas/5
        [ResponseType(typeof(DetalleFormula))]
        public IHttpActionResult DeleteDetalleFormula(int id)
        {
            DetalleFormula detalleFormula = db.DetalleFormulas.Find(id);
            if (detalleFormula == null)
            {
                return NotFound();
            }

            db.DetalleFormulas.Remove(detalleFormula);
            db.SaveChanges();

            return Ok(detalleFormula);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DetalleFormulaExists(int id)
        {
            return db.DetalleFormulas.Count(e => e.DetalleFormulaID == id) > 0;
        }
    }
}