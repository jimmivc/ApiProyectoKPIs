using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiProyectoKPI.Models
{
    public class FormasContacto
    {

        public FormasContacto()
        {

        }

        public int FormasContactoID { get; set; }
        public Prospecto Prospecto { get; set; }
        public int Item { get; set; }
        public GrupoEmpresarial GrupoEmpresarial { get; set; }
        public TipoFormaContacto TipoFormaContacto { get; set; }
        public string DescFormaContacto { get; set; }
        public bool IsHabilitado { get; set; }
        
         

    }
}