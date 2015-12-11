using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiProyectoKPI.Models
{
    public class Categoria
    {
        
        public Categoria()
        {

        }

        public int CategoriaID { get; set; }
        public string DescCategoria { get; set; }
        public int PuntajeMinimo{ get; set; }
        public int PuntajeMaximo { get; set; }

        }
}