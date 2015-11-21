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
    public class BitacorasController : ApiController
    {
        private ApiKPIsContext db = new ApiKPIsContext();

        // GET: api/Bitacoras
        public IQueryable<Bitacora> GetBitacoras()
        {
            return db.Bitacoras;
        }

        // GET: api/Bitacoras/5
        [ResponseType(typeof(Bitacora))]
        public IHttpActionResult GetBitacora(int id)
        {
            Bitacora bitacora = db.Bitacoras.Find(id);
            if (bitacora == null)
            {
                return NotFound();
            }

            return Ok(bitacora);
        }

        // PUT: api/Bitacoras/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBitacora(int id, Bitacora bitacora)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bitacora.BitacoraID)
            {
                return BadRequest();
            }

            db.Entry(bitacora).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BitacoraExists(id))
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

        // POST: api/Bitacoras
        [ResponseType(typeof(Bitacora))]
        public IHttpActionResult PostBitacora(Bitacora bitacora)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bitacora.FechaHora = DateTime.Now;
            db.Bitacoras.Add(bitacora);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bitacora.BitacoraID }, bitacora);
        }

        // DELETE: api/Bitacoras/5
        [ResponseType(typeof(Bitacora))]
        public IHttpActionResult DeleteBitacora(int id)
        {
            Bitacora bitacora = db.Bitacoras.Find(id);
            if (bitacora == null)
            {
                return NotFound();
            }

            db.Bitacoras.Remove(bitacora);
            db.SaveChanges();

            return Ok(bitacora);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BitacoraExists(int id)
        {
            return db.Bitacoras.Count(e => e.BitacoraID == id) > 0;
        }

        public void registrarBitacora(int idUsuario,string evento)
        {
            Bitacora bitacora = new Bitacora(idUsuario, evento);
            db.Bitacoras.Add(bitacora);
        }

    }
}