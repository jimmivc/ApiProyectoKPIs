using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiProyectoKPI.Models
{
    public class EncuestaMaestro
    {
        public EncuestaMaestro()
        {
            
        }

        public EncuestaMaestro(int id, RelacionCursos relacionCursos, Plantilla plantilla, DateTime fechaVence, List<EncuestaDetalle> detalle)
        {
            this.EncuestaMaestroID = id;
            this.RelacionCursos = relacionCursos;
            this.PlantillaEncuestaMaestro = plantilla;
            this.Fecha = DateTime.Now;
            this.FechaVence = fechaVence;
            this.Estatus = true;
            this.EncuestaDetalle = detalle;
        }
        public int EncuestaMaestroID { get; set; }
        public RelacionCursos RelacionCursos { get; set; }
        public Plantilla PlantillaEncuestaMaestro { get; set; }
        public DateTime? Fecha { get; set; }
        public string Observaciones { get; set; }
        public DateTime? FechaVence { get; set; }
        public bool Estatus { get; set; }
        public ICollection<EncuestaDetalle> EncuestaDetalle { get; set; }
    }
}