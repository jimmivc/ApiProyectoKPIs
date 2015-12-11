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
    public class PreguntasController : ApiController
    {
        private ApiKPIsContext db = new ApiKPIsContext();

        // GET: api/Preguntas
        public IQueryable<Pregunta> GetPreguntas()
        {
            return db.Preguntas.Include(c => c.Categoria);
        }

        [HttpGet]
        [Route("api/PreguntasCategoria")]
        public IQueryable<Pregunta> GetPreguntasCategoria()
        {
            return db.Preguntas.Include(b => b.Categoria);
        }

        // GET: api/Preguntas/5
        [ResponseType(typeof(Pregunta))]
        public IHttpActionResult GetPregunta(int id)
        {
            Pregunta pregunta = db.Preguntas.Find(id);
            if (pregunta == null)
            {
                return NotFound();
            }

            return Ok(pregunta);
        }

        // PUT: api/Preguntas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPregunta(int id, Pregunta pregunta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pregunta.PreguntaID)
            {
                return BadRequest();
            }

            db.Entry(pregunta).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreguntaExists(id))
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

        // POST: api/Preguntas
        [ResponseType(typeof(Pregunta))]
        public IHttpActionResult PostPregunta(Pregunta pregunta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (pregunta.Categoria == null)
            {
                pregunta.Categoria = null;
            }
            else
            {
                pregunta.Categoria = db.Categorias.Find(pregunta.Categoria.CategoriaID);
            }
            db.Preguntas.Add(pregunta);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pregunta.PreguntaID }, pregunta);
        }
        
        // DELETE: api/Preguntas/5
        [ResponseType(typeof(Pregunta))]
        public IHttpActionResult DeletePregunta(int id)
        {
            Pregunta pregunta = db.Preguntas.Find(id);
            if (pregunta == null)
            {
                return NotFound();
            }

            db.Preguntas.Remove(pregunta);
            db.SaveChanges();

            return Ok(pregunta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PreguntaExists(int id)
        {
            return db.Preguntas.Count(e => e.PreguntaID == id) > 0;
        }
    }
}