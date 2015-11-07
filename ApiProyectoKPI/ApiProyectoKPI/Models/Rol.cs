using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiProyectoKPI.Models
{
    public class Rol
    {
        public Rol()
        {
            IndicadoresKPI = new HashSet<KPI>();
            Permisos = new HashSet<Permiso>();

        }

        public int RolID { get; set; }
        public string Nombre { get; set; }
        public ICollection<Permiso> Permisos { get; set; }
        [JsonIgnore]
        public ICollection<Usuario> Usuarios { get; set; }
        public ICollection<KPI> IndicadoresKPI { get; set; }
    }
}