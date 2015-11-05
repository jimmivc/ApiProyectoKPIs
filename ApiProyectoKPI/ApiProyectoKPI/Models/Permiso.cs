using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiProyectoKPI.Models
{
    public class Permiso
    {
        public Permiso()
        {
            Roles = new HashSet<Rol>();
        }

        public int PermisoID { get; set; }
        public string Descripcion { get; set; }
        public string pAccion { get; set; }
        public ICollection<Rol> Roles { get; set; }
    }
}
