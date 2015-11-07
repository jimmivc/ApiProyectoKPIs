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
    public class KPIsController : ApiController
    {
        private ApiKPIsContext db = new ApiKPIsContext();

        // GET: api/KPIs
        public IQueryable<KPI> GetKPIs()
        {
            return db.KPIs.Where(b => b.Estado == true);
        }

        // GET: api/KPIs/5
        [ResponseType(typeof(KPI))]
        public IHttpActionResult GetKPI(int id)
        {
            KPI kPI = db.KPIs.Where(b => b.KPIID == id).Include(b => b.Parametro).Include(b => b.Formula).FirstOrDefault();
            if (kPI == null)
            {
                return NotFound();
            }

            return Ok(kPI);
        }

        // PUT: api/KPIs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKPI(int id, KPI kPI)
        {
        
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kPI.KPIID)
            {
                return BadRequest();
            }

            db.Entry(kPI).State = EntityState.Modified;

            //KPI kpiModificado = db.KPIs.Where(b => b.KPIID == id).Include(b => b.Parametro).FirstOrDefault();
            //List<DetalleFormula> formulas = kPI.Formula.ToList<DetalleFormula>();
            //List<DetalleFormula> ids = kpiModificado.Formula.ToList<DetalleFormula>();

            //DetalleFormulasController formulaController = new DetalleFormulasController();

            //for (int i = 0; i < kpiModificado.Formula.Count; i++){
            //    formula.DeleteDetalleFormula(ids[i].DetalleFormulaID);
            //}
            //for (int i = 0; i < kPI.Formula.Count; i++)
            //{
            //    formulaController.PutDetalleFormula()
            //    formulas[i].KPI = kPI;
            //    formula.PostDetalleFormula(formulas[i]);
            //}
            //kPI.Formula = formulas;
            //kPI.Parametro.ParametroKPIID = kpiModificado.Parametro.ParametroKPIID;
            //kPI.Formula = null;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KPIExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.OK);
        }

        // PUT: api/KPIs/5
        [HttpPut]
        [Route("api/KPIs/deshabilitar/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeshabilitarKPI(int id)
        {

            KPI kPI = db.KPIs.Where(b=>b.KPIID == id).Include(b=>b.Parametro).FirstOrDefault();
            kPI.Estado = false;
            db.Entry(kPI).State = EntityState.Modified;

            //KPI kpiModificado = db.KPIs.Where(b => b.KPIID == id).Include(b => b.Parametro).FirstOrDefault();
            //List<DetalleFormula> formulas = kPI.Formula.ToList<DetalleFormula>();
            //List<DetalleFormula> ids = kpiModificado.Formula.ToList<DetalleFormula>();

            //DetalleFormulasController formulaController = new DetalleFormulasController();

            //for (int i = 0; i < kpiModificado.Formula.Count; i++){
            //    formula.DeleteDetalleFormula(ids[i].DetalleFormulaID);
            //}
            //for (int i = 0; i < kPI.Formula.Count; i++)
            //{
            //    formulaController.PutDetalleFormula()
            //    formulas[i].KPI = kPI;
            //    formula.PostDetalleFormula(formulas[i]);
            //}
            //kPI.Formula = formulas;
            //kPI.Parametro.ParametroKPIID = kpiModificado.Parametro.ParametroKPIID;
            //kPI.Formula = null;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KPIExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.OK);
        }

        // POST: api/KPIs
        [ResponseType(typeof(KPI))]
        public IHttpActionResult PostKPI(KPI kPI)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            kPI.Estado = true;
            db.KPIs.Add(kPI);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.OK);
            //CreatedAtRoute("DefaultApi", new { id = kPI.KPIID }, kPI);
        }

        // DELETE: api/KPIs/5
        [ResponseType(typeof(KPI))]
        public IHttpActionResult DeleteKPI(int id)
        {
            KPI kPI = db.KPIs.Find(id);
            if (kPI == null)
            {
                return NotFound();
            }

            db.KPIs.Remove(kPI);
            db.SaveChanges();

            return Ok(kPI);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KPIExists(int id)
        {
            return db.KPIs.Count(e => e.KPIID == id) > 0;
        }

    }
}