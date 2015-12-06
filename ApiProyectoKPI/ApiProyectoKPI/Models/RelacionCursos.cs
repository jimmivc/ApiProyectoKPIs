using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApiProyectoKPI.Models
{
    public class RelacionCursos
    {

        public RelacionCursos()
        {

        }

        public int RelacionCursosID { get; set; }
        public Curso Curso { get; set; }
        public Usuario Estudiante { get; set; }
        public Usuario Profesor { get; set; }
        public string Horario { get; set; }
        public int Cuatrimestre { get; set; }
        public int Anio { get; set; }



    }
}