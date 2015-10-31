using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiProyectoKPI.Models
{
    public class Evento
    {

        public Evento()
        {
            FechaEvento = DateTime.Now;
        }
        public int EventoID { get; set; }
        public string DescEventoCaptacion { get; set; }
        public DateTime FechaEvento { get; set; }
        public string LugarEvento { get; set; }
    }
}