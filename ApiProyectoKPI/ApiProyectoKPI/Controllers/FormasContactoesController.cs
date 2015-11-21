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
    public class FormasContactoesController : ApiController
    {
        private ApiKPIsContext db = new ApiKPIsContext();
        /// <summary>
        /// GetFormasContactoes.  
        /// Devuelve una lista de las formas de contacto.
        /// </summary>
        /// <returns>Respuesta con la lista de FormasContacto.</returns>
        /// <remarks><para>Historia de Creación y modificaciones:
        /// <list type="bullet">
        /// <item>Autor.: Christian Ulloa Tosso </item>
        /// <item>07/11/2015 - Creación</item>
        /// </list></para></remarks>
        // GET: api/FormasContactoes
        public IQueryable<FormasContacto> GetFormasContactoes()
        {
            return db.FormasContactoes;
        }
        /// <summary>
        /// GetIsFormasContacto.  
        /// Devuelve un objeto de formas de contacto sin objetos internos.
        /// </summary>
        /// <param name="Id">parámetro de tipo Integer.</param>
        /// <returns>Respuesta con el objeto formas de contacto.</returns>
        /// <remarks><para>Historia de Creación y modificaciones:
        /// <list type="bullet">
        /// <item>Autor.: Christian Ulloa Tosso </item>
        /// <item>07/11/2015 - Creación</item>
        /// </list></para></remarks>
        
        // GET: api/FormasContactoes/is/id
        [Route("api/FormasContactoes/is/{id}")]
        [ResponseType(typeof(FormasContacto))]
        public IHttpActionResult GetIsFormasContacto(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                FormasContacto formasContacto = db.FormasContactoes.Find(id);
                if (formasContacto == null)
                {
                    return NotFound();
                }
                return Ok(formasContacto);
            }
        }

        /// <summary>
        /// GetFormasContacto.  
        /// Levanta el objeto FormasContacto con sus objetos internos.
        /// </summary>
        /// <param name="Id">parámetro de tipo Integer.</param>
        /// <returns>Respuesta con el objeto FormasContacto.</returns>
        /// <remarks><para>Historia de Creación y modificaciones:
        /// <list type="bullet">
        /// <item>Autor.: Christian Ulloa Tosso </item>
        /// <item>07/11/2015 - Creación</item>
        /// </list></para></remarks>

        // GET: api/FormasContactoes/5
        [ResponseType(typeof(FormasContacto))]
        public IHttpActionResult GetFormasContacto(int id)
        {
            FormasContacto formasContacto = db.FormasContactoes.Where(i => i.FormasContactoID == id).
                Include(e => e.TipoFormaContacto).Include(g => g.GrupoEmpresarial).FirstOrDefault();
            if (formasContacto == null)
            {
                return NotFound();
            }

            return Ok(formasContacto);
        }
        /// <summary>
        /// PutFormasContacto.  
        /// Modificar una forma de contacto existente.
        /// </summary>
        /// <param name="Id">parámetro de tipo Integer.</param>
        /// <param name="formasContacto">parámetro de tipo FormasContacto.</param>
        /// <remarks><para>Historia de Creación y modificaciones:
        /// <list type="bullet">
        /// <item>Autor.: Christian Ulloa Tosso </item>
        /// <item>07/11/2015 - Creación</item>
        /// </list></para></remarks>
        // PUT: api/FormasContactoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFormasContacto(int id, FormasContacto formasContacto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != formasContacto.FormasContactoID)
            {
                return BadRequest();
            }
            formasContacto.TipoFormaContacto = db.TipoFormaContactoes.Find(formasContacto.TipoFormaContacto.TipoFormaContactoID);
            if (formasContacto.GrupoEmpresarial == null)
            {
                formasContacto.GrupoEmpresarial = null;
            }
            else
            {
                formasContacto.GrupoEmpresarial = db.GrupoEmpresarials.Find(formasContacto.GrupoEmpresarial.GrupoEmpresarialID);
            }
            formasContacto.Prospecto = null;
            db.Entry(formasContacto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormasContactoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = formasContacto.FormasContactoID }, formasContacto);
        }

        /// <summary>
        /// PostFormasContacto.  
        /// Registras una forma de contacto nuevo.
        /// </summary>
        /// <param name="formasContacto">parámetro de tipo FormasContacto.</param>
        /// <remarks><para>Historia de Creación y modificaciones:
        /// <list type="bullet">
        /// <item>Autor.: Christian Ulloa Tosso </item>
        /// <item>07/11/2015 - Creación</item>
        /// </list></para></remarks>
        // POST: api/FormasContactoes
        [ResponseType(typeof(FormasContacto))]
        public IHttpActionResult PostFormasContacto(FormasContacto formasContacto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            formasContacto.TipoFormaContacto = db.TipoFormaContactoes.Find(formasContacto.TipoFormaContacto.TipoFormaContactoID);
            if (formasContacto.GrupoEmpresarial == null )
            {
                formasContacto.GrupoEmpresarial = null;
            }
            else
            {
                formasContacto.GrupoEmpresarial = db.GrupoEmpresarials.Find(formasContacto.GrupoEmpresarial.GrupoEmpresarialID);
            }
            formasContacto.Prospecto = db.Prospectoes.Find(formasContacto.Prospecto.ProspectoID);
            //db.Configuration.AutoDetectChangesEnabled = false;
            db.FormasContactoes.Add(formasContacto);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = formasContacto.FormasContactoID }, formasContacto);
        }

        // POST: api/FormasContactoes/FormasContactoes/RegistraListaFormasContacto
        [Route("api/FormasContactoes/RegistraListaFormasContacto")]
        [ResponseType(typeof(List<FormasContacto>))]
        public IHttpActionResult PostListaFormasContacto(List<FormasContacto> formasContactos)
        {
            if (!(formasContactos == null))
            {
                foreach (FormasContacto f in formasContactos)
                {
                    FormasContacto formasContacto = f;
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    formasContacto.TipoFormaContacto = db.TipoFormaContactoes.Find(formasContacto.TipoFormaContacto.TipoFormaContactoID);
                    if (formasContacto.GrupoEmpresarial == null)
                    {
                        formasContacto.GrupoEmpresarial = null;
                    }
                    else
                    {
                        formasContacto.GrupoEmpresarial = db.GrupoEmpresarials.Find(formasContacto.GrupoEmpresarial.GrupoEmpresarialID);
                    }
                    formasContacto.Prospecto = db.Prospectoes.Find(formasContacto.Prospecto.ProspectoID);
                    //db.Configuration.AutoDetectChangesEnabled = false;
                    db.FormasContactoes.Add(formasContacto);
                    db.SaveChanges();
                }
            }



            return StatusCode(HttpStatusCode.OK);
        }





        /// <summary>
        /// DeleteFormasContacto.  
        /// Eliminar una forma de contacto.
        /// </summary>
        /// <param name="Id">parámetro de tipo Integer.</param>
        /// <remarks><para>Historia de Creación y modificaciones:
        /// <list type="bullet">
        /// <item>Autor.: Christian Ulloa Tosso </item>
        /// <item>07/11/2015 - Creación</item>
        /// </list></para></remarks>
        // DELETE: api/FormasContactoes/5
        [ResponseType(typeof(FormasContacto))]
        public IHttpActionResult DeleteFormasContacto(int id)
        {
            FormasContacto formasContacto = db.FormasContactoes.Find(id);
            if (formasContacto == null)
            {
                return NotFound();
            }

            db.FormasContactoes.Remove(formasContacto);
            db.SaveChanges();

            return Ok(formasContacto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FormasContactoExists(int id)
        {
            return db.FormasContactoes.Count(e => e.FormasContactoID == id) > 0;
        }
    }
}