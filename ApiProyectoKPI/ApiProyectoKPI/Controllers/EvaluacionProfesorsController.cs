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
    public class EvaluacionProfesorsController : ApiController
    {
        private ApiKPIsContext db = new ApiKPIsContext();

        // GET: api/EvaluacionProfesors
        public IQueryable<EvaluacionProfesor> GetEvaluacionProfesors()
        {
            return db.EvaluacionProfesors;
        }

        // GET: api/EvaluacionProfesors/5
        [ResponseType(typeof(EvaluacionProfesor))]
        public IHttpActionResult GetEvaluacionProfesor(int id)
        {
            EvaluacionProfesor evaluacionProfesor = db.EvaluacionProfesors.Find(id);
            if (evaluacionProfesor == null)
            {
                return NotFound();
            }

            return Ok(evaluacionProfesor);
        }

        // PUT: api/EvaluacionProfesors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEvaluacionProfesor(int id, EvaluacionProfesor evaluacionProfesor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != evaluacionProfesor.EvaluacionProfesorID)
            {
                return BadRequest();
            }

            db.Entry(evaluacionProfesor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvaluacionProfesorExists(id))
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

        // POST: api/EvaluacionProfesors
        [ResponseType(typeof(EvaluacionProfesor))]
        public IHttpActionResult PostEvaluacionProfesor(EvaluacionProfesor evaluacionProfesor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EvaluacionProfesors.Add(evaluacionProfesor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = evaluacionProfesor.EvaluacionProfesorID }, evaluacionProfesor);
        }

        // DELETE: api/EvaluacionProfesors/5
        [ResponseType(typeof(EvaluacionProfesor))]
        public IHttpActionResult DeleteEvaluacionProfesor(int id)
        {
            EvaluacionProfesor evaluacionProfesor = db.EvaluacionProfesors.Find(id);
            if (evaluacionProfesor == null)
            {
                return NotFound();
            }

            db.EvaluacionProfesors.Remove(evaluacionProfesor);
            db.SaveChanges();

            return Ok(evaluacionProfesor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EvaluacionProfesorExists(int id)
        {
            return db.EvaluacionProfesors.Count(e => e.EvaluacionProfesorID == id) > 0;
        }
    }
}