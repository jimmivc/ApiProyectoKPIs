using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiProyectoKPI.Models
{
    public class Seguimiento
    {
        public Seguimiento()
        {
            FechaHora = DateTime.Now;
        }

        public int SeguimientoID{ get; set; }
        public DateTime FechaHora { get; set; }
        public Prospecto Prospecto { get; set; }
        public string DescSeguimiento { get; set; }
        public int NumeroLlamadas { get; set; }
        public int DuracionLlamadaMinutos { get; set; }
        public bool IsEfectiva { get; set; }
        public bool IsFormaContactoValida { get; set; }
        public Usuario Usuario { get; set; }
    }
}