﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiProyectoKPI.Models
{
    public class Usuario
    {
        public Usuario()
        {

        }

        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public bool IsActivo { get; set; }

    }
}