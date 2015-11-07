using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;



namespace ApiProyectoKPI.Models
{
    
    public class Prospecto
    {
        public Prospecto()
        {
            FechaNac = DateTime.Now;
            FechaIngresoBase = DateTime.Now;
        }
        public int ProspectoID { get; set; }
        public string Identificacion { get; set; }
        public string Alias { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }
        public DateTime FechaNac { get; set; }
        public int AnioBachillerato { get; set; }
        public DateTime FechaIngresoBase { get; set; }
        public Evento Evento { get; set; }
        public bool IsTrabajando { get; set; }
        public bool IsInscritoPromociones { get; set; }
        public string LugarEstudioAnterior { get; set; }
        public string LugarTrabajo { get; set; }
        public bool IsHabilitado { get; set; }
        public Usuario Usuario { get; set; }
        [JsonIgnore]
        public ICollection<FormasContacto> FormasContacto { get; set; }
        [JsonIgnore]
        public ICollection<AreaInteres> AreasInteres { get; set; }




    }
}