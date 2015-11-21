using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApiProyectoKPI.Models
{
    public class DetalleFormula
    {
        public DetalleFormula()
        {

        }

        public int DetalleFormulaID { get; set; }
        
        public int Consecutivo { get; set; }

        public string TipoDato { get; set; }
        
        public string Tabla { get; set; }
        public string DescCampoOperador { get; set; }
        public double? Valor { get; set; }
        [JsonIgnore]
        public KPI KPI { get; set; }
    }
}