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
using ApiProyectoKPI.Controllers;

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

        [HttpGet]
        [Route("api/RelacionCursos/Generar/{idProfesor}/{cuatrimestre}/{anio}/{curso}/{plantilla}")]
        //[ResponseType(typeof(RelacionCursos))]
        public IHttpActionResult GetGenerarRelacionCursosEncuesta(int idProfesor, int cuatrimestre, int anio, int curso, int plantilla)
        {
            List<EncuestaMaestro> listaEncuestas = GenerarRelacionCursosEncuesta(idProfesor,cuatrimestre,anio,curso,plantilla);
            foreach (EncuestaMaestro l in listaEncuestas)
            {
                PostEncuestaMaestro(l);
            }
            return Ok(listaEncuestas);
            
        }

       
        //[ResponseType(typeof(RelacionCursos))]
        public List<EncuestaMaestro> GenerarRelacionCursosEncuesta(int idProfesor, int cuatrimestre, int anio, int curso, int plantilla)
        {
            EncuestaMaestro encuestaMaestro = new EncuestaMaestro();
            List<EncuestaMaestro> listaEncuestas = new List<EncuestaMaestro>();
            var detallePreguntas = db.PlantillaDetalles.Include(d => d.Plantilla).Include(t => t.Pregunta).Where(p => p.Plantilla.PlantillaID == 1).ToList();
            var machote = db.Plantillas.Find(plantilla);
            var listaRelacionCursos = db.RelacionCursos.Where(p => p.Profesor.UsuarioID == idProfesor).
                Where(c => c.Cuatrimestre == cuatrimestre).Where(a => a.Anio == anio).Where(u => u.Curso.CursoID == curso);
            
            foreach (RelacionCursos r in listaRelacionCursos)
            {
                
                var detallesEncuesta = new List<EncuestaDetalle>();
                foreach (PlantillaDetalle p in detallePreguntas)
                {
                    EncuestaDetalle encuestaDetalle = new EncuestaDetalle(0, p.Pregunta, 0);
                    detallesEncuesta.Add(encuestaDetalle);
                }
                encuestaMaestro = new EncuestaMaestro(0,r,machote,DateTime.Now,detallesEncuesta);
                listaEncuestas.Add(encuestaMaestro);
               }
            return listaEncuestas;
        }



        // POST: api/EncuestaDetalles
        [ResponseType(typeof(EncuestaDetalle))]
        public IHttpActionResult PostEncuestaDetalle(EncuestaDetalle encuestaDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EncuestaDetalles.Add(encuestaDetalle);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = encuestaDetalle.EncuestaDetalleID }, encuestaDetalle);
        }

        

        // POST: api/EncuestaMaestroes
        [ResponseType(typeof(EncuestaMaestro))]
        public IHttpActionResult PostEncuestaMaestro(EncuestaMaestro encuestaMaestro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EncuestaMaestroes.Add(encuestaMaestro);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = encuestaMaestro.EncuestaMaestroID }, encuestaMaestro);
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