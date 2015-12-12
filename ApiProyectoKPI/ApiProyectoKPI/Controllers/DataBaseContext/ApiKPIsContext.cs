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
        public ApiKPIsContext (): base("name=KPIsConnectionString")
        {
            this.Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<ApiKPIsContext>(new DatosStandard());
            
        }


        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.AreaInteres> AreaInteres { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.GrupoEmpresarial> GrupoEmpresarials { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.Evento> Eventoes { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.FormasContacto> FormasContactoes { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.Prospecto> Prospectoes { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.TipoFormaContacto> TipoFormaContactoes { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.Usuario> Usuarios { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.Seguimiento> Seguimientoes { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.Bitacora> Bitacoras { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.DetalleFormula> DetalleFormulas { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.KPI> KPIs { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.ParametroKPI> ParametroKPIs { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.Permiso> Permisoes { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.Rol> Rols { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.RegistroMercadeo> RegistrosMercadeo { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.Venta> Ventas { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.Curso> Cursoes { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.RelacionCursos> RelacionCursos { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.TipoCurso> TipoCursoes { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.Pregunta> Preguntas { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.Categoria> Categorias { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.EncuestaMaestro> EncuestaMaestroes { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.EncuestaDetalle> EncuestaDetalles { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.Plantilla> Plantillas { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.PlantillaDetalle> PlantillaDetalles { get; set; }

        public System.Data.Entity.DbSet<ApiProyectoKPI.Models.EvaluacionProfesor> EvaluacionProfesors { get; set; }
    }
}