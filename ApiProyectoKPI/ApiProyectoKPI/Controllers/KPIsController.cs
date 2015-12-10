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
    public class KPIsController : ApiController
    {
        private ApiKPIsContext db = new ApiKPIsContext();

        // GET: api/KPIs
        public IQueryable<KPI> GetKPIs()
        {
            return db.KPIs.Where(b => b.Estado == true);
        }

        // GET: api/KPIs/5
        [ResponseType(typeof(KPI))]
        public IHttpActionResult GetKPI(int id)
        {
            KPI kPI = db.KPIs.Where(b => b.KPIID == id).Include(b => b.Parametro).Include(b => b.Formula).FirstOrDefault();
            if (kPI == null)
            {
                return NotFound();
            }

            return Ok(kPI);
        }

        // PUT: api/KPIs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKPI(int id, KPI kPI)
        {
        
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kPI.KPIID)
            {
                return BadRequest();
            }

            db.Entry(kPI).State = EntityState.Modified;
            
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KPIExists(id))
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

        // PUT: api/KPIs/5
        [HttpPut]
        [Route("api/KPIs/deshabilitar/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeshabilitarKPI(int id)
        {

            KPI kPI = db.KPIs.Where(b=>b.KPIID == id).Include(b=>b.Parametro).FirstOrDefault();
            kPI.Estado = false;
            db.Entry(kPI).State = EntityState.Modified;
            
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KPIExists(id))
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

        // POST: api/KPIs
        [ResponseType(typeof(KPI))]
        public IHttpActionResult PostKPI(KPI kPI)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            kPI.Estado = true;
            db.KPIs.Add(kPI);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.OK);
            
        }

        // DELETE: api/KPIs/5
        [ResponseType(typeof(KPI))]
        public IHttpActionResult DeleteKPI(int id)
        {
            KPI kPI = db.KPIs.Find(id);
            if (kPI == null)
            {
                return NotFound();
            }

            db.KPIs.Remove(kPI);
            db.SaveChanges();

            return Ok(kPI);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KPIExists(int id)
        {
            return db.KPIs.Count(e => e.KPIID == id) > 0;
        }

        /// <summary>
        /// asignarKPI
        /// metodo encargado de relacionar un kpi con un rol
        /// </summary>
        /// <param name="idKPI">int</param>
        /// <param name="idRol">int</param>
        /// <returns>resultado de la consulta</returns>
        /// <remarks><para>Historia de Creación y modificaciones:
        /// <list type="bullet">
        /// <item>Autor.: Jimmi Vila </item>
        /// <item>10/10/2015 - Creación</item>
        /// </list></para></remarks>
        [HttpGet]
        [Route("api/KPIs/asignar/{idKPI}/{idRol}")]
        public HttpResponseMessage asignarKPI(int idKPI, int idRol){
            KPI kpi = db.KPIs.Find(idKPI);
            Rol rol = db.Rols.Find(idRol);

            if (kpi != null && rol != null)
            {
                rol.IndicadoresKPI.Add(kpi);
                kpi.RolesAsignados.Add(rol);
                
                try{
                db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    
                    return Request.CreateResponse<string>(HttpStatusCode.InternalServerError,"El indicador ya fue asignado al rol seleccionado"); 
                }
                
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        /// <summary>
        /// indicadoresAsignados
        /// metodo encargado de obtener la lista de indicadores asignados a un rol
        /// </summary>
        /// <param name="idRol">int</param>
        /// <returns>double</returns>
        /// <remarks><para>Historia de Creación y modificaciones:
        /// <list type="bullet">
        /// <item>Autor.: Jimmi Vila </item>
        /// <item>10/10/2015 - Creación</item>
        /// </list></para></remarks>
        [HttpGet]
        [Route("api/KPIs/indicadoresAsignados/{idRol}")]
        public IQueryable<KPI> indicadoresAsignados(int idRol)
        {
            var kpis = db.KPIs
                             .Where(x => x.RolesAsignados.Any(r => idRol == (r.RolID))).Where(b=>b.Estado == true);
            return kpis;   
        }
        /// <summary>
        /// resultadosKPI
        /// metodo encargado de obtener el resultado de un indicador kpi
        /// </summary>
        /// <param name="idRol">int</param>
        /// <param name="idRegistro">int</param>
        /// <returns>double</returns>
        /// <remarks><para>Historia de Creación y modificaciones:
        /// <list type="bullet">
        /// <item>Autor.: Jimmi Vila </item>
        /// <item>10/10/2015 - Creación</item>
        /// </list></para></remarks>
        [HttpGet]
        [Route("api/KPIs/resultados/{idRol}/{idRegistro}")]
        public List<List<string>> resultadosKPI(int idRol, int idRegistro)
        {
            List<List<string>> datos = new List<List<string>>();

            var usuarios = db.Usuarios
                .Where(b=>b.Rol.RolID == idRol);
            var registro = db.RegistrosMercadeo.Find(idRegistro);
            //colocar la hora 12:00 en todos los registros
            var registros = db.RegistrosMercadeo.Where(b=>b.fechaHora == registro.fechaHora);
        
            var kp = indicadoresAsignados(idRol).Include(b=>b.Parametro).Include(b=>b.Formula);
            List<KPI> kpis = kp.ToList();
            if (registro != null && usuarios != null)
            {
                foreach (KPI k in kpis)
                {
                    datos.Add(k.calcularResultados(registros.ToList<RegistroMercadeo>(), usuarios.ToList<Usuario>()));
                }
            }

            return datos;
        }
        /// <summary>
        /// Desasignar
        /// </summary>
        /// <param name="idKPI"></param>
        /// <param name="idRol"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/KPIs/desasignar/{idKPI}/{idRol}")]
        public HttpResponseMessage desasignarKPI(int idKPI, int idRol)
        {
            KPI kpi = db.KPIs.Where(b => b.KPIID == idKPI).Include(b => b.RolesAsignados).FirstOrDefault();
            Rol rol = db.Rols.Where(b =>b.RolID == idRol).Include(b=>b.IndicadoresKPI).FirstOrDefault();
            

            if (kpi != null && rol != null)
            {
                
                rol.IndicadoresKPI.Remove(kpi);
                kpi.RolesAsignados.Remove(rol);

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {

                    return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, "El indicador ya fue asignado al rol seleccionado");
                }

            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        //basado en fechas!!!!!!!!!!
        //[HttpGet]
        //[Route("api/KPIs/resultados/{idRol}")]
        //public List<List<string>> resultadosKPI(int idRol)
        //{
        //    List<List<string>> datos = new List<List<string>>();

        //    var usuarios = db.Usuarios
        //        .Where(b => b.Rol.RolID == idRol);
        //    var registro = db.RegistrosMercadeo.Find(idRegistro);
        //    //colocar la hora 12:00 en todos los registros
        //    var registros = db.RegistrosMercadeo.Where(b => b.fechaHora == registro.fechaHora);

        //    var kp = indicadoresAsignados(idRol).Include(b => b.Parametro).Include(b => b.Formula);
        //    List<KPI> kpis = kp.ToList();
        //    if (registro != null && usuarios != null)
        //    {
        //        foreach (KPI k in kpis)
        //        {
        //            datos.Add(k.calcularResultados(registros.ToList<RegistroMercadeo>(), usuarios.ToList<Usuario>()));
        //        }
        //    }

        //    return datos;
        //}
    }
}