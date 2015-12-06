using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiProyectoKPI.Models
{
    public class Pregunta
    {


        public Pregunta()
        {

        }

        public int PreguntaID { get; set; }
        public string DescPregunta { get; set; }
        public Categoria Categoria { get; set; }
       

    }
}


