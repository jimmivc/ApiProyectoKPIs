using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiProyectoKPI.Models
{
    public class Bitacora
    {
        public Bitacora(){
            FechaHora = DateTime.Now;
        }
        public Bitacora(int idUsuario, string accion)
        {
            Usuario = new Usuario();
            Usuario.UsuarioID = idUsuario;
            Accion = accion;
        }

        public int BitacoraID { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime FechaHora { get; set; }
        public string Accion { get; set; }

    }
}