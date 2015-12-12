using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiProyectoKPI.Models
{
    public class EvaluacionProfesor
    {
        public EvaluacionProfesor()
        {

        }

        public int EvaluacionProfesorID { get; set; }
        public double NotaPromedio { get; set; }
        public Curso Curso { get; set; }
        public Usuario Profesor { get; set; }
        public int Cuatrimestre { get; set; }
        public int Anio { get; set; }
    }
}