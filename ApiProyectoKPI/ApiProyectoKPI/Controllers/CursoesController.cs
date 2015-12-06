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
    public class CursoesController : ApiController
    {
        private ApiKPIsContext db = new ApiKPIsContext();

        /// <summary>
        /// PostListaCursos.  
        /// Registra una lista de cursos.
        /// </summary>
        /// <param name="cursos">parámetro de tipo Lista de Cursos.</param>
        /// <remarks><para>Historia de Creación y modificaciones:
        /// <list type="bullet">
        /// <item>Autor.: Christian Ulloa Tosso </item>
        /// <item>20/11/2015 - Creación</item>
        /// </list></para></remarks>
        // POST: api/Cursoes/RegistraListaCursos
        [Route("api/Cursoes/RegistraListaCursos")]
        [ResponseType(typeof(List<Curso>))]
        public IHttpActionResult PostListaCursos(List<Curso> cursos)
        {
            if (!(cursos == null))
            {
                foreach (Curso c in cursos)
                {
                    Curso curso = c;

                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    if (curso == null)
                    {
                        return NotFound();
                    }
                    //prospecto.Evento = null;
                    if (curso.TipoCurso == null)
                    {
                        curso.TipoCurso = null;
                    }
                    else
                    {
                        curso.TipoCurso = db.TipoCursoes.Find(curso.TipoCurso .TipoCursoID);
                    }

                    //db.Configuration.AutoDetectChangesEnabled = false;

                    db.Cursoes.Add(curso );

                    db.SaveChanges();
                }
            }
                 return StatusCode(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("api/Cursoes/Codigo/{id}")]
        public IHttpActionResult GetCursoXCodigo(string id)
        {
            Curso curso = db.Cursoes.Where(i => i.CodigoCurso == id).FirstOrDefault();
            if (curso == null)
            {
                return NotFound();
            }

            return Ok(curso);
        }

        // GET: api/Cursoes
        public IQueryable<Curso> GetCursoes()
        {
            return db.Cursoes.Include(t => t.TipoCurso);
        }

        // GET: api/Cursoes/5
        [ResponseType(typeof(Curso))]
        public IHttpActionResult GetCurso(int id)
        {
            Curso curso = db.Cursoes.Find(id);
            if (curso == null)
            {
                return NotFound();
            }

            return Ok(curso);
        }

        // PUT: api/Cursoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCurso(int id, Curso curso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != curso.CursoID)
            {
                return BadRequest();
            }

            db.Entry(curso).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursoExists(id))
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

        // POST: api/Cursoes
        [ResponseType(typeof(Curso))]
        public IHttpActionResult PostCurso(Curso curso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cursoes.Add(curso);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = curso.CursoID }, curso);
        }

        // DELETE: api/Cursoes/5
        [ResponseType(typeof(Curso))]
        public IHttpActionResult DeleteCurso(int id)
        {
            Curso curso = db.Cursoes.Find(id);
            if (curso == null)
            {
                return NotFound();
            }

            db.Cursoes.Remove(curso);
            db.SaveChanges();

            return Ok(curso);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CursoExists(int id)
        {
            return db.Cursoes.Count(e => e.CursoID == id) > 0;
        }
    }
}