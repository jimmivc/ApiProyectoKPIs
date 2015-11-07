using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiProyectoKPI.Models
{
    public class RegistroMercadeo
    {
        public RegistroMercadeo()
        {

        }
        public int RegistroMercadeoID { get; set; }
        public DateTime fechaHora { get; set; }
        public int TotalLlamadas { get; set; }
        public int TotalLlamadasEfectivas { get; set; }
        public int PromDuraLlamadasEfectivas { get; set; }
        public int DuracionLlamadaEfectiva { get; set; }
        public int CantidadVentas { get; set; }
        public double MontoTotalVentas { get; set; }
        public Usuario usuario { get; set; }
    }
}