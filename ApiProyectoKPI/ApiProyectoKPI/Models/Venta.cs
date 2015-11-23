using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiProyectoKPI.Models
{
    public class Venta
    {
        public Venta()
        {

            Prospecto = new Prospecto();

        }

        public int VentaID { get; set; }

        public DateTime Fecha { get; set; }
        public string Periodo { get; set; }
        public string Categoria { get; set; }
        public String Descripcion { get; set; }
        public double Monto { get; set; }


        public Prospecto Prospecto { get; set; }
        public Usuario Usuario { get; set; }

    }
}