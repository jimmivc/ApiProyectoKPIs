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
    public class RelacionCursosController : ApiController
    {
        private ApiKPIsContext db = new ApiKPIsContext();

        /// <summary>
        /// PostListaRelacionCursos.  
        /// Registra una lista de relación de cursos.
        /// </summary>
        /// <param name="relacionesCursos">parámetro de tipo Lista de Cursos.</param>
        /// <remarks><para>Historia de Creación y modificaciones:
        /// <list type="bullet">
        /// <item>Autor.: Christian Ulloa Tosso </item>
        /// <item>20/11/2015 - Creación</item>
        /// </list></para></remarks>
        // POST: api/Cursoes/RegistraListaCursos
        [Route("api/RelacionCursos/RegistraListaCursos")]
        [ResponseType(typeof(List<RelacionCursos>))]
        public IHttpActionResult PostListaRelacionCursos(List<RelacionCursos> relacionesCursos)
        {
            if (!(relacionesCursos == null))
            {
                foreach (RelacionCursos r in relacionesCursos)
                {
                    RelacionCursos relacionCurso = r;

                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    if (relacionCurso == null)
                    {
                        return NotFound();
                    }
                    //Toma Curso
                    if (relacionCurso.Curso == null)
                    {
                        relacionCurso.Curso  = null;
                    }
                    else
                    {
                        relacionCurso.Curso = db.Cursoes.Find(relacionCurso.Curso.CursoID);
                    }
                    //Toma Profesor
                    if (relacionCurso.Profesor == null)
                    {
                        relacionCurso.Profesor = null;
                    }
                    else
                    {
                        relacionCurso.Profesor = db.Usuarios.Find(relacionCurso.Profesor.UsuarioID);
                    }
                    //Toma Estudiante
                    if (relacionCurso.Estudiante  == null)
                    {
                        relacionCurso.Estudiante  = null;
                    }
                    else
                    {
                        relacionCurso.Estudiante = db.Usuarios.Find(relacionCurso.Estudiante.UsuarioID);
                    }

                    //db.Configuration.AutoDetectChangesEnabled = false;

                    db.RelacionCursos.Add(relacionCurso);

                    db.SaveChanges();
                }
            }
            return StatusCode(HttpStatusCode.OK);
        }


        // GET: api/RelacionCursos
        public IQueryable<RelacionCursos> GetRelacionCursos()
        {
            return db.RelacionCursos;
        }

        // GET: api/RelacionCursos/5
        [ResponseType(typeof(RelacionCursos))]
        public IHttpActionResult GetRelacionCursos(int id)
        {
            RelacionCursos relacionCursos = db.RelacionCursos.Find(id);
            if (relacionCursos == null)
            {
                return NotFound();
            }

            return Ok(relacionCursos);
        }

        // PUT: api/RelacionCursos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRelacionCursos(int id, RelacionCursos relacionCursos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != relacionCursos.RelacionCursosID)
            {
                return BadRequest();
            }

            db.Entry(relacionCursos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelacionCursosExists(id))
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

        // POST: api/RelacionCursos
        [ResponseType(typeof(RelacionCursos))]
        public IHttpActionResult PostRelacionCursos(RelacionCursos relacionCursos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (relacionCursos.Curso == null)
            {
                relacionCursos.Curso = null;
            }
            else
            {
                relacionCursos.Curso = db.Cursoes.Find(relacionCursos.Curso.CursoID);
            }

            db.RelacionCursos.Add(relacionCursos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = relacionCursos.RelacionCursosID }, relacionCursos);
        }

        // DELETE: api/RelacionCursos/5
        [ResponseType(typeof(RelacionCursos))]
        public IHttpActionResult DeleteRelacionCursos(int id)
        {
            RelacionCursos relacionCursos = db.RelacionCursos.Find(id);
            if (relacionCursos == null)
            {
                return NotFound();
            }

            db.RelacionCursos.Remove(relacionCursos);
            db.SaveChanges();

            return Ok(relacionCursos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RelacionCursosExists(int id)
        {
            return db.RelacionCursos.Count(e => e.RelacionCursosID == id) > 0;
        }
    }
}