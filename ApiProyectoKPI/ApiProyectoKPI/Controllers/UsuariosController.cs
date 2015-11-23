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
    public class UsuariosController : ApiController
    {
        private ApiKPIsContext db = new ApiKPIsContext();

        // GET: api/Usuarios
        public IQueryable<Usuario> GetUsuarios()
        {
            return db.Usuarios.Include(b => b.Rol);
        }

        // GET: api/Usuarios/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult GetUsuario(int id)
        {
            Usuario usuario = db.Usuarios.Where(b => b.UsuarioID == id).Include(b => b.Rol).FirstOrDefault();
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // GET: api/Usuarios/5
        [HttpGet]
        [Route("api/Usuarios/correo/{id}/{a}")]
        public IHttpActionResult GetUsuarioCorreo(string id)
        {
            Usuario usuario = db.Usuarios.Where(b => b.Correo.Equals(id)).FirstOrDefault();
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // PUT: api/Usuarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsuario(int id, Usuario usuario)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.UsuarioID)
            {
                return BadRequest();
            }

            db.Entry(usuario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult PostUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            usuario.Rol = db.Rols.Find(usuario.Rol.RolID);

            db.Usuarios.Add(usuario);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.OK);

            //return CreatedAtRoute("DefaultApi", new { id = usuario.UsuarioID }, usuario);
        }

        /// <summary>
        /// PostListaProspecto.  
        /// Registra una lista de prospectos.
        /// </summary>
        /// <param name="prospectos">parámetro de tipo Lista de Prospecto.</param>
        /// <remarks><para>Historia de Creación y modificaciones:
        /// <list type="bullet">
        /// <item>Autor.: Christian Ulloa Tosso </item>
        /// <item>07/11/2015 - Creación</item>
        /// </list></para></remarks>
        // POST: api/Prospectoes/RegistraListaProspectos
        [Route("api/Usuarios/RegistraListaUsuarios")]
        [ResponseType(typeof(List<Usuario>))]
        public IHttpActionResult PostListaUsuarios(List<Usuario> usuarios)
        {
            if (!(usuarios == null))
            {
                foreach (Usuario u in usuarios)
                {
                    Usuario usuario = u;

                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    if (usuario  == null)
                    {
                        return NotFound();
                    }
                    //prospecto.Evento = null;
                    if (usuario.Rol == null)
                    {
                        usuario.Rol = null;
                    }
                    else
                    {
                        usuario.Rol = db.Rols.Find(usuario.Rol.RolID);
                    }

                    //db.Configuration.AutoDetectChangesEnabled = false;

                    db.Usuarios.Add(usuario);

                    db.SaveChanges();
                }
            }
        return StatusCode(HttpStatusCode.OK);
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
        [Route("api/Usuarios/identificacion/{id}")]
        public IHttpActionResult GetUsuarioIdentificacion(int id)
        {
            Usuario usuario = db.Usuarios.Where(i => i.Cedula == id).FirstOrDefault();
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // DELETE: api/Usuarios/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult DeleteUsuario(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            db.Usuarios.Remove(usuario);
            db.SaveChanges();

            return Ok(usuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsuarioExists(int id)
        {
            return db.Usuarios.Count(e => e.UsuarioID == id) > 0;
        }
    }
}