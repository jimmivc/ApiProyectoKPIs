using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiProyectoKPI.Models
{
    public class EncuestaDetalle
    {
        public EncuestaDetalle()
        {


        }

        public EncuestaDetalle(int id, Pregunta pregunta, int puntaje)
        {
            EncuestaDetalleID = 0;
            Pregunta = pregunta;
            Puntaje = puntaje;
        }
        public int EncuestaDetalleID { get; set; }
        public Pregunta Pregunta { get; set; }
        public int Puntaje { get; set; }
    }
}