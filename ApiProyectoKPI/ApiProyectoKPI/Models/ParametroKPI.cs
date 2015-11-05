using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApiProyectoKPI.Models
{
    public class ParametroKPI
    {
        public ParametroKPI()
        {

        }
        [Key]
        public int ParametroKPIID { get; set; }

        public int LimiteSuperior { get; set; }
        public int LimiteInferior { get; set; }
        public KPI KPIAsignado { get; set; }
        
    }
}