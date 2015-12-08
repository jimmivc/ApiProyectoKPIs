using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiProyectoKPI.Models
{
    public class PlantillaDetalle
    {
        public PlantillaDetalle() {

        }
        public int PlantillaDetalleID { get; set; }
        public Plantilla Plantilla { get; set; }
        public Pregunta Pregunta { get; set; }
    }
}