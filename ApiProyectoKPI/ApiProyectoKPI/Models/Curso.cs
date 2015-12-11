using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiProyectoKPI.Models
{
    public class Curso
    {

        public Curso()
        {

        }

        public int CursoID { get; set; }
        public string CodigoCurso { get; set; }
        public string NombreCurso { get; set; }
        public TipoCurso TipoCurso { get; set; }



    }
}