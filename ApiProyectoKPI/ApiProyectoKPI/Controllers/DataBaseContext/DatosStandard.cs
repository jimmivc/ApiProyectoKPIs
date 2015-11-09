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

            base.Seed(context);
        }
    }
}