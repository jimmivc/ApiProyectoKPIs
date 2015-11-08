using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace ApiProyectoKPI.Models
{
    public class KPI
    {
        public KPI()
        {
            RolesAsignados = new HashSet<Rol>();
        }

        public int KPIID { get; set; }
        public string DescKpi { get; set; }
        public string Formato { get; set; }
        public double Objetivo { get; set; }
        public bool Estado { get; set; }

        [Required]
        public ParametroKPI Parametro { get; set; }
        public ICollection<DetalleFormula> Formula { get; set; }
        
        public ICollection<Rol> RolesAsignados { get; set; }

        public String calcularResultados(List<RegistroMercadeo> registros, List<Usuario> usuarios){
            return "gg";
        }
    }
}