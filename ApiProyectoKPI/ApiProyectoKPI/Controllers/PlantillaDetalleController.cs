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
    public class PlantillaDetalleController : ApiController
    {
        private ApiKPIsContext db = new ApiKPIsContext();

        // GET: api/PlantillaDetalle
        public IQueryable<PlantillaDetalle> GetPlantillaDetalles()
        {
            return db.PlantillaDetalles;
        }

        // GET: api/PlantillaDetalle/5
        [ResponseType(typeof(PlantillaDetalle))]
        public IHttpActionResult GetPlantillaDetalle(int id)
        {
            PlantillaDetalle plantillaDetalle = db.PlantillaDetalles.Find(id);
            if (plantillaDetalle == null)
            {
                return NotFound();
            }

            return Ok(plantillaDetalle);
        }

        // PUT: api/PlantillaDetalle/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlantillaDetalle(int id, PlantillaDetalle plantillaDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != plantillaDetalle.PlantillaDetalleID)
            {
                return BadRequest();
            }

            db.Entry(plantillaDetalle).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantillaDetalleExists(id))
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

        // POST: api/PlantillaDetalle
        [ResponseType(typeof(PlantillaDetalle))]
        public IHttpActionResult PostPlantillaDetalle(PlantillaDetalle plantillaDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Encuentra la plantilla ya existente
            if (plantillaDetalle.Plantilla == null)
            {
                plantillaDetalle.Plantilla = null;
            }
            else
            {
                plantillaDetalle.Plantilla = db.Plantillas.Find(plantillaDetalle.Plantilla.PlantillaID);
            }
            //Encuentra la pregunta ya existente
            if (plantillaDetalle.Pregunta == null)
            {
                plantillaDetalle.Pregunta = null;
            }
            else
            {
                plantillaDetalle.Pregunta = db.Preguntas.Find(plantillaDetalle.Pregunta.PreguntaID);
            }
            //Salva
            db.PlantillaDetalles.Add(plantillaDetalle);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.OK);
            //return CreatedAtRoute("DefaultApi", new { id = plantillaDetalle.PlantillaDetalleID }, plantillaDetalle);
        }

        // DELETE: api/PlantillaDetalle/5
        [ResponseType(typeof(PlantillaDetalle))]
        public IHttpActionResult DeletePlantillaDetalle(int id)
        {
            PlantillaDetalle plantillaDetalle = db.PlantillaDetalles.Find(id);
            if (plantillaDetalle == null)
            {
                return NotFound();
            }

            db.PlantillaDetalles.Remove(plantillaDetalle);
            db.SaveChanges();

            return Ok(plantillaDetalle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlantillaDetalleExists(int id)
        {
            return db.PlantillaDetalles.Count(e => e.PlantillaDetalleID == id) > 0;
        }
    }
}