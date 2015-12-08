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
    public class PlantillasController : ApiController
    {
        private ApiKPIsContext db = new ApiKPIsContext();

        // GET: api/Plantillas
        public IQueryable<Plantilla> GetPlantillas()
        {
            return db.Plantillas;
        }

        // GET: api/Plantillas/5
        [ResponseType(typeof(Plantilla))]
        public IHttpActionResult GetPlantilla(int id)
        {
            Plantilla plantilla = db.Plantillas.Find(id);
            if (plantilla == null)
            {
                return NotFound();
            }

            return Ok(plantilla);
        }

        // PUT: api/Plantillas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlantilla(int id, Plantilla plantilla)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != plantilla.PlantillaID)
            {
                return BadRequest();
            }

            db.Entry(plantilla).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantillaExists(id))
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

        // POST: api/Plantillas
        [ResponseType(typeof(Plantilla))]
        public IHttpActionResult PostPlantilla(Plantilla plantilla)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Plantillas.Add(plantilla);
            db.SaveChanges();

            Plantilla newPlantilla = plantilla;

            return Ok(newPlantilla);

            //return CreatedAtRoute("DefaultApi", new { id = plantilla.PlantillaID }, plantilla);
        }

        // DELETE: api/Plantillas/5
        [ResponseType(typeof(Plantilla))]
        public IHttpActionResult DeletePlantilla(int id)
        {
            Plantilla plantilla = db.Plantillas.Find(id);
            if (plantilla == null)
            {
                return NotFound();
            }

            db.Plantillas.Remove(plantilla);
            db.SaveChanges();

            return Ok(plantilla);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlantillaExists(int id)
        {
            return db.Plantillas.Count(e => e.PlantillaID == id) > 0;
        }
    }
}