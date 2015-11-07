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

        // GET: api/FormasContactoes
        public IQueryable<FormasContacto> GetFormasContactoes()
        {
            return db.FormasContactoes;
        }

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

            return StatusCode(HttpStatusCode.NoContent);
        }

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