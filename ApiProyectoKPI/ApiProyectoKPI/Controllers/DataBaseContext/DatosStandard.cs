using ApiProyectoKPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ApiProyectoKPI.Controllers.DataBaseContext
{
    public class DatosStandard: CreateDatabaseIfNotExists<ApiKPIsContext>//DropCreateDatabaseAlways<ApiKPIsContext>
    {
        protected override void Seed(ApiKPIsContext context)
        {
            IList<Rol> defaultRols = new List<Rol>();

            defaultRols.Add(new Rol() { Nombre = "Administrador" });
            defaultRols.Add(new Rol() { Nombre = "Soporte" });
            defaultRols.Add(new Rol() { Nombre = "Mercadeo" });
            
            foreach (Rol rol in defaultRols)
                context.Rols.Add(rol);
            context.Usuarios.Add(new Usuario(){Nombre = "Jimmi", Apellidos = "Vila", Cedula = 160400 ,Correo = "jvilac@ucenfotec.ac.cr",Contrasena = "tXFOeepeTOFiXdv5UUVUBA==", IsActivo = true, Rol = context.Rols.Find(1)});
            
            base.Seed(context);
        }
    }
}