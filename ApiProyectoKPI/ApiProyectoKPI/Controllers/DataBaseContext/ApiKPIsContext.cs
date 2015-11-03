using ApiProyectoKPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ApiProyectoKPI.Controllers.DataBaseContext
{
    public class ApiKPIsContext:DbContext
    {
        public ApiKPIsContext (): base("name=KPIsStoreConnectionString")
        {
            Database.SetInitializer<ApiKPIsContext>(new  DropCreateDatabaseIfModelChanges<ApiKPIsContext>());
        }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.AreaInteres> AreaInteres { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.GrupoEmpresarial> GrupoEmpresarials { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.Evento> Eventoes { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.FormasContacto> FormasContactoes { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.Prospecto> Prospectoes { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.TipoFormaContacto> TipoFormaContactoes { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.Usuario> Usuarios { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.Seguimiento> Seguimientoes { get; set; }
    }
}