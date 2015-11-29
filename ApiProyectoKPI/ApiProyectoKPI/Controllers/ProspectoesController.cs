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
using System.Web.Routing;

namespace ApiProyectoKPI.Controllers
{
    public class ProspectoesController : ApiController
    {
        private ApiKPIsContext db = new ApiKPIsContext();
        /// <summary>
        /// GetProspectoes.  
        /// Obtener una lista de prospectos con su objeto evento cargado.
        /// </summary>
        /// <returns>Respuesta con una lista de prospectos.</returns>
        /// <remarks><para>Historia de Creación y modificaciones:
        /// <list type="bullet">
        /// <item>Autor.: Christian Ulloa Tosso </item>
        /// <item>07/11/2015 - Creación</item>
        /// </list></para></remarks>
        // GET: api/Prospectoes
        public IQueryable<Prospecto> GetProspectoes()
        {

            return db.Prospectoes.Include (e => e.Evento);

        }


        /// <item>Autor.: Marjorie Arce </item>
        // GET: api/Prospectoes/Usuarios
        [HttpGet]
        [Route("api/Prospectoes/Usuarios/")]
        public IQueryable<Prospecto> prospectoUsuario()
        {

            return db.Prospectoes.Include(e => e.Usuario).Include(b => b.Usuario.Rol).Where(r => r.Usuario.Rol.RolID == 3);

        }

        /// <item>Autor.: Marjorie Arce </item>
        // GET: api/Prospectoes/asignar
        [HttpGet]
        [Route("api/Prospectoes/asignar/{idUsuario}/{idProspecto}")]
        public HttpResponseMessage asignarUsuario(int idUsuario, int idProspecto)
        {
            Usuario usuario = db.Usuarios.Find(idUsuario);
            Prospecto prospecto = db.Prospectoes.Find(idProspecto);

            if (usuario != null && prospecto != null)
            {


                prospecto.Usuario = usuario;
                prospecto.ProspectoID = idProspecto;

                db.Prospectoes.Find(prospecto.ProspectoID);

                db.SaveChanges();

            }

            try
            {
                db.SaveChanges();
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, "El usuario fue asignado efectivamente al prospecto correspondiente");
            }
            catch (DbUpdateException up)
            {

                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, "El usuario ya fue asignado");
            }

        }


        /// <summary>
        /// GetIsProspecto.  
        /// Devuelve un objeto de prospecto sin objetos internos.
        /// </summary>
        /// <param name="Id">parámetro de tipo Integer.</param>
        /// <returns>Respuesta con el objeto Prospecto.</returns>
        /// <remarks><para>Historia de Creación y modificaciones:
        /// <list type="bullet">
        /// <item>Autor.: Christian Ulloa Tosso </item>
        /// <item>07/11/2015 - Creación</item>
        /// </list></para></remarks>
        // GET: api/Prospectoes/is/id
        [Route("api/Prospectoes/is/{id}")]
        [ResponseType(typeof(Prospecto))]
        public IHttpActionResult GetIsProspecto(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                Prospecto prospecto = db.Prospectoes.Find(id);
                if (prospecto == null)
                {
                    return NotFound();
                }
                return Ok(prospecto);
            }
        }

        /// <summary>
        /// GetProspecto.  
        /// Devuelve un objeto de prospecto con sus una lista de objetos de formas de contacto dentro y a su vez las formas de contacto 
        /// con sus objetos internos TiposFormaContacto y GrupoEmpresarial dentro de las mismas.
        /// </summary>
        /// <param name="Id">parámetro de tipo Integer.</param>
        /// <returns>Respuesta con el objeto Prospecto.</returns>
        /// <remarks><para>Historia de Creación y modificaciones:
        /// <list type="bullet">
        /// <item>Autor.: Christian Ulloa Tosso </item>
        /// <item>07/11/2015 - Creación</item>
        /// </list></para></remarks>
        // GET: api/Prospectoes/5
        [ResponseType(typeof(Prospecto))]
        public IHttpActionResult GetProspecto(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                Prospecto prospecto = db.Prospectoes.Where(i => i.ProspectoID == id).Include(e => e.Evento).FirstOrDefault();
                db.Entry(prospecto).Collection(f => f.FormasContactos).Load();
                foreach (FormasContacto f in prospecto.FormasContactos)
                {
                    FormasContacto formaConTipo = db.FormasContactoes.Where(i => i.FormasContactoID == f.FormasContactoID).
                                                  Include(t => t.TipoFormaContacto).FirstOrDefault();
                    f.TipoFormaContacto = formaConTipo.TipoFormaContacto;
                }
                foreach (FormasContacto f in prospecto.FormasContactos)
                {
                    FormasContacto formaConGrupo = db.FormasContactoes.Where(i => i.FormasContactoID == f.FormasContactoID).
                                                  Include(t => t.GrupoEmpresarial).FirstOrDefault();
                    f.GrupoEmpresarial = formaConGrupo.GrupoEmpresarial;
                }



                if (prospecto == null)
                {
                    return NotFound();
                }

                return Ok(prospecto);
            }
        }
        /// <summary>
        /// GetProspectoSeguimiento.  
        /// Devuelve un objeto de prospecto su lista de registro de seguimientos.
        /// </summary>
        /// <param name="Id">parámetro de tipo Integer.</param>
        /// <returns>Respuesta con el objeto Prospecto.</returns>
        /// <remarks><para>Historia de Creación y modificaciones:
        /// <list type="bullet">
        /// <item>Autor.: Christian Ulloa Tosso </item>
        /// <item>07/11/2015 - Creación</item>
        /// </list></para></remarks>
        //GET: api/Prospectoes/5
        [Route("api/Prospectoes/Seguimiento/{id}")]
        [ResponseType(typeof(Prospecto))]
        public IHttpActionResult GetProspectoSeguimiento(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                Prospecto prospecto = db.Prospectoes.Where(i => i.ProspectoID == id).FirstOrDefault();
                db.Entry(prospecto).Collection(f => f.Seguimientos).Load();
                foreach (Seguimiento f in prospecto.Seguimientos)
                {
                    Seguimiento seguimientoConUsuario = db.Seguimientoes.Where(i => i.SeguimientoID == f.SeguimientoID).
                                                  Include(t => t.Usuario).FirstOrDefault();
                    f.Usuario = seguimientoConUsuario.Usuario;
                }
                if (prospecto == null)
                {
                    return NotFound();
                }

                return Ok(prospecto);
            }
        }
        /// <summary>
        /// GetUsuarioIdentificacion.  
        /// Devuelve un objeto de prospecto cuyo número de identificación sea el requerido.
        /// </summary>
        /// <param name="Id">parámetro de tipo Integer.</param>
        /// <returns>Respuesta con el objeto Prospecto.</returns>
        /// <remarks><para>Historia de Creación y modificaciones:
        /// <list type="bullet">
        /// <item>Autor.: Christian Ulloa Tosso </item>
        /// <item>07/11/2015 - Creación</item>
        /// </list></para></remarks>
        // GET: api/Prospectoes/identificacion/id
        [HttpGet]
        [Route("api/Prospectoes/identificacion/{id}")]
        public IHttpActionResult GetUsuarioIdentificacion(string id)
        {
            Prospecto prospecto = db.Prospectoes.Where(i => i.Identificacion == id).FirstOrDefault();
            if (prospecto == null)
            {
                return NotFound();
            }

            return Ok(prospecto);
        }
        /// <summary>
        /// GetProspectoIden.  
        /// Devuelve un objeto de prospecto sin objetos internos.
        /// </summary>
        /// <param name="Id">parámetro de tipo Integer.</param>
        /// <returns>Respuesta con el objeto Prospecto.</returns>
        /// <remarks><para>Historia de Creación y modificaciones:
        /// <list type="bullet">
        /// <item>Autor.: Christian Ulloa Tosso </item>
        /// <item>07/11/2015 - Creación</item>
        /// </list></para></remarks>
        // GET: api/Prospectoes/iden/id
        [Route("api/Prospectoes/iden/{id}")]
        [ResponseType(typeof(Prospecto))]
        public IHttpActionResult GetProspectoIden(int iden)
        {
            Prospecto prospecto = db.Prospectoes.Find(iden);

            if (prospecto == null)
            {
                return NotFound();
            }

            return Ok(prospecto);
        }

        /// <summary>
        /// PutProspecto.  
        /// Modifica un prospecto existente.
        /// </summary>
        /// <param name="Id">parámetro de tipo Integer.</param>
        /// <param name="prospecto">parámetro de tipo Prospecto.</param>
        /// <returns>Respuesta con el objeto Prospecto.</returns>
        /// <remarks><para>Historia de Creación y modificaciones:
        /// <list type="bullet">
        /// <item>Autor.: Christian Ulloa Tosso </item>
        /// <item>07/11/2015 - Creación</item>
        /// </list></para></remarks>
        // PUT: api/Prospectoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProspecto(int id, Prospecto prospecto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prospecto.ProspectoID)
            {
                return BadRequest();
            }
            
            Evento newEvento = db.Eventoes.Find(prospecto.Evento.EventoID);
            prospecto.Evento = newEvento;
            db.Eventoes.Attach(prospecto.Evento);
            db.Prospectoes.Attach(prospecto);
           
          //  db.Prospectoes.Attach(prospecto);
           // prospecto.Evento = db.Eventoes.Find(prospecto.Evento.EventoID)
            //db.Prospectoes.Attach(prospecto);

            db.Entry(prospecto).CurrentValues.SetValues(prospecto);
                       
            db.Entry(prospecto).State = EntityState.Modified ;
            db.Entry(prospecto.Evento).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProspectoExists(id))
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



        /// <summary>
        /// PostProspecto.  
        /// Registra un nuevo prospecto.
        /// </summary>
        /// <param name="prospecto">parámetro de tipo Prospecto.</param>
        /// <returns>Respuesta con el objeto Prospecto.</returns>
        /// <remarks><para>Historia de Creación y modificaciones:
        /// <list type="bullet">
        /// <item>Autor.: Christian Ulloa Tosso </item>
        /// <item>07/11/2015 - Creación</item>
        /// </list></para></remarks>
        // POST: api/Prospectoes
        [ResponseType(typeof(Prospecto))]
        public IHttpActionResult PostProspecto(Prospecto prospecto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (prospecto == null)
            {
                return NotFound();
            }
            //prospecto.Evento = null;
            if (prospecto.Evento == null)
            {
                prospecto.Evento = null;
            }
            else
            {
                prospecto.Evento = db.Eventoes.Find(prospecto.Evento.EventoID);
            }

            //db.Configuration.AutoDetectChangesEnabled = false;
           
            db.Prospectoes.Add(prospecto);
            
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = prospecto.ProspectoID }, prospecto);
        }

        // POST: api/Prospectoes/RegistraListaProspectos
        [Route("api/Prospectoes/RegistraListaProspectos")]
        [ResponseType(typeof(List<Prospecto>))]
        public IHttpActionResult PostListaProspecto(List<Prospecto> prospectos)
        {
            if (!(prospectos == null))
            {
                foreach (Prospecto p in prospectos)
                {
                    Prospecto prospecto = p;

                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    if (prospecto == null)
                    {
                        return NotFound();
                    }
                    //prospecto.Evento = null;
                    if (prospecto.Evento == null)
                    {
                        prospecto.Evento = null;
                    }
                    else
                    {
                        prospecto.Evento = db.Eventoes.Find(prospecto.Evento.EventoID);
                    }

                    //db.Configuration.AutoDetectChangesEnabled = false;

                    db.Prospectoes.Add(prospecto);

                    db.SaveChanges();
                }
            }
           
            

            return StatusCode(HttpStatusCode.OK);
        }



        /// <summary>
        /// DeleteProspecto.  
        /// Elimina un prospecto existente.
        /// </summary>
        /// <param name="id">parámetro de tipo Integer.</param>
        /// <returns>Respuesta con el objeto Prospecto.</returns>
        /// <remarks><para>Historia de Creación y modificaciones:
        /// <list type="bullet">
        /// <item>Autor.: Christian Ulloa Tosso </item>
        /// <item>07/11/2015 - Creación</item>
        /// </list></para></remarks>
        // DELETE: api/Prospectoes/5
        [ResponseType(typeof(Prospecto))]
        public IHttpActionResult DeleteProspecto(int id)
        {
            Prospecto prospecto = db.Prospectoes.Find(id);
            if (prospecto == null)
            {
                return NotFound();
            }

            db.Prospectoes.Remove(prospecto);
            db.SaveChanges();

            return Ok(prospecto);
        }

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProspectoExists(int id)
        {
            return db.Prospectoes.Count(e => e.ProspectoID == id) > 0;
        }
    }
}