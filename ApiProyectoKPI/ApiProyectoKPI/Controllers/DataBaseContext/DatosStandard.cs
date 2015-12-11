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

            IList<Permiso> defaultPermisos = new List<Permiso>();
            defaultPermisos.Add(new Permiso() { PermisoID = 1, pAccion = "Operaciones" });
            defaultPermisos.Add(new Permiso() { PermisoID = 2, pAccion = "GestionProspectos" });
            defaultPermisos.Add(new Permiso() { PermisoID = 3, pAccion = "ImportarProspectos" });
            defaultPermisos.Add(new Permiso() { PermisoID = 4, pAccion = "ImportarUsuarios" });
            defaultPermisos.Add(new Permiso() { PermisoID = 5, pAccion = "GenerarRegistroMercadeo" });
            defaultPermisos.Add(new Permiso() { PermisoID = 6, pAccion = "AsignarKpis" });
            defaultPermisos.Add(new Permiso() { PermisoID = 7, pAccion = "GestionarVentas" });
            defaultPermisos.Add(new Permiso() { PermisoID = 8, pAccion = "Configuracion" });
            defaultPermisos.Add(new Permiso() { PermisoID = 9, pAccion = "Eventos" });
            defaultPermisos.Add(new Permiso() { PermisoID = 10, pAccion = "Entidades" });
            defaultPermisos.Add(new Permiso() { PermisoID = 11, pAccion = "Kpis" });
            defaultPermisos.Add(new Permiso() { PermisoID = 12, pAccion = "Reportes" });
            defaultPermisos.Add(new Permiso() { PermisoID = 13, pAccion = "IngresosUsuario" });
            defaultPermisos.Add(new Permiso() { PermisoID = 14, pAccion = "Ventas" });
            defaultPermisos.Add(new Permiso() { PermisoID = 15, pAccion = "IndicadoresKpi" });
            defaultPermisos.Add(new Permiso() { PermisoID = 16, pAccion = "FuentesProspectos" });
            defaultPermisos.Add(new Permiso() { PermisoID = 17, pAccion = "Seguridad" });
            defaultPermisos.Add(new Permiso() { PermisoID = 18, pAccion = "Usuarios" });
            defaultPermisos.Add(new Permiso() { PermisoID = 19, pAccion = "Roles" });
            defaultPermisos.Add(new Permiso() { PermisoID = 20, pAccion = "Permisos" });

            foreach (Permiso permiso in defaultPermisos)
            {
                context.Permisoes.Add(permiso);
            }
            
            IList<Rol> defaultRols = new List<Rol>();

            defaultRols.Add(new Rol() { Nombre = "Administrador", RolID = 1, Permisos = new List<Permiso> { 
                context.Permisoes.Find(1),
                context.Permisoes.Find(2),
                context.Permisoes.Find(3),
                context.Permisoes.Find(4),
                context.Permisoes.Find(5),
                context.Permisoes.Find(6),
                context.Permisoes.Find(7),
                context.Permisoes.Find(8),
                context.Permisoes.Find(9),
                context.Permisoes.Find(10),
                context.Permisoes.Find(11),
                context.Permisoes.Find(12),
                context.Permisoes.Find(13),
                context.Permisoes.Find(14),
                context.Permisoes.Find(15),
                context.Permisoes.Find(16),
                context.Permisoes.Find(17),
                context.Permisoes.Find(18),
                context.Permisoes.Find(19),
                context.Permisoes.Find(20) } });
            defaultRols.Add(new Rol() { Nombre = "Soporte", RolID = 2 });
            defaultRols.Add(new Rol() { Nombre = "Mercadeo", RolID = 3 });
            defaultRols.Add(new Rol() { Nombre = "Profesor", RolID = 4 });
            defaultRols.Add(new Rol() { Nombre = "Estudiante", RolID = 5 });

            foreach (Rol rol in defaultRols)
            {
                context.Rols.Add(rol);
            }
            IList<TipoFormaContacto> defaultTiposFormaContacto = new List<TipoFormaContacto>();
            defaultTiposFormaContacto.Add(new TipoFormaContacto() { DescTipoFormaContacto = "Teléfono movil" });
            defaultTiposFormaContacto.Add(new TipoFormaContacto() { DescTipoFormaContacto = "Teléfono oficina" });
            defaultTiposFormaContacto.Add(new TipoFormaContacto() { DescTipoFormaContacto = "Teléfono casa" });
            defaultTiposFormaContacto.Add(new TipoFormaContacto() { DescTipoFormaContacto = "Correo electrónico" });
            defaultTiposFormaContacto.Add(new TipoFormaContacto() { DescTipoFormaContacto = "Red social" });
            defaultTiposFormaContacto.Add(new TipoFormaContacto() { DescTipoFormaContacto = "Skype" });
            defaultTiposFormaContacto.Add(new TipoFormaContacto() { DescTipoFormaContacto = "Fax" });
            defaultTiposFormaContacto.Add(new TipoFormaContacto() { DescTipoFormaContacto = "Chat" });
            defaultTiposFormaContacto.Add(new TipoFormaContacto() { DescTipoFormaContacto = "Apartado postal" });

            IList<Evento> defaultEventos = new List<Evento>();
            defaultEventos.Add(new Evento() {DescEventoCaptacion = "No definida", LugarEvento = "" });
            defaultEventos.Add(new Evento() { DescEventoCaptacion = "Feria U", LugarEvento = "Center Interuniversity" });
            defaultEventos.Add(new Evento() { DescEventoCaptacion = "Feria El Saber", LugarEvento = "Fercori" });
            defaultEventos.Add(new Evento() { DescEventoCaptacion = "Seminario vocacional", LugarEvento = "Cenfotec" });
            defaultEventos.Add(new Evento() { DescEventoCaptacion = "Feria Científica Universitaria", LugarEvento = "Earth" });
            defaultEventos.Add(new Evento() { DescEventoCaptacion = "Feria Estudia y Empleate", LugarEvento = "Fercori" });

            IList<GrupoEmpresarial> defaultGruposEmpresariales = new List<GrupoEmpresarial>();
            defaultGruposEmpresariales.Add(new GrupoEmpresarial () { DescGrupoEmpresarial  = "IBM" });
            defaultGruposEmpresariales.Add(new GrupoEmpresarial() { DescGrupoEmpresarial = "Intel" });
            defaultGruposEmpresariales.Add(new GrupoEmpresarial() { DescGrupoEmpresarial = "Artinsoft" });
            defaultGruposEmpresariales.Add(new GrupoEmpresarial() { DescGrupoEmpresarial = "Softland" });
            defaultGruposEmpresariales.Add(new GrupoEmpresarial() { DescGrupoEmpresarial = "Análisis MBC" });
            defaultGruposEmpresariales.Add(new GrupoEmpresarial() { DescGrupoEmpresarial = "Innova Solutions" });

            
            context.Usuarios.Add(new Usuario() { UsuarioID=1, Nombre = "Jimmi", Apellidos = "Vila", Cedula = 160400, Correo = "jvilac@ucenfotec.ac.cr", Contrasena = "tXFOeepeTOFiXdv5UUVUBA==", IsActivo = true, Rol = context.Rols.Find(1) });
            context.Usuarios.Add(new Usuario() { UsuarioID=2, Nombre = "Christian", Apellidos = "Ulloa", Cedula = 109150113, Correo = "culloat@ucenfotec.ac.cr", Contrasena = "tXFOeepeTOFiXdv5UUVUBA==", IsActivo = true, Rol = context.Rols.Find(3) });
            context.Usuarios.Add(new Usuario() { UsuarioID=3, Nombre = "Hernán", Apellidos = "Sáenz", Cedula = 99999999, Correo = "gsaenzp@ucenfotec.ac.cr", Contrasena = "tXFOeepeTOFiXdv5UUVUBA==", IsActivo = true, Rol = context.Rols.Find(3) });
            context.Usuarios.Add(new Usuario() { UsuarioID=4, Nombre = "Alvaro", Apellidos = "Cordero", Cedula = 99999999, Correo = "acordero@ucenfotec.ac.cr", Contrasena = "tXFOeepeTOFiXdv5UUVUBA==", IsActivo = true, Rol = context.Rols.Find(3) });
            context.Usuarios.Add(new Usuario() { UsuarioID=5, Nombre = "Alonso", Apellidos = "Guevara", Cedula = 99999999, Correo = "aguevara@ucenfotec.ac.cr", Contrasena = "tXFOeepeTOFiXdv5UUVUBA==", IsActivo = true, Rol = context.Rols.Find(3) });
            foreach (TipoFormaContacto  tipo in defaultTiposFormaContacto)
            {
                context.TipoFormaContactoes.Add(tipo);
            }
            foreach (Evento evento in defaultEventos)
            {
                context.Eventoes.Add(evento);
            }
            foreach (GrupoEmpresarial  empresa in defaultGruposEmpresariales)
            {
                context.GrupoEmpresarials.Add(empresa);
            }

            IList<RegistroMercadeo> defaultRegistrosMercadeo = new List<RegistroMercadeo>();
            defaultRegistrosMercadeo.Add(new RegistroMercadeo() { usuario = context.Usuarios.Find(2), CantidadVentas = 10, DuracionLlamadaEfectiva = 10, fechaHora = new DateTime(2015, 11, 1), MontoTotalVentas = 100000, PromDuraLlamadasEfectivas = 10, TotalLlamadas = 40, TotalLlamadasEfectivas = 12 });
            defaultRegistrosMercadeo.Add(new RegistroMercadeo() { usuario = context.Usuarios.Find(3), CantidadVentas = 10, DuracionLlamadaEfectiva = 10, fechaHora = new DateTime(2015, 11, 1), MontoTotalVentas = 200000, PromDuraLlamadasEfectivas = 12, TotalLlamadas = 30, TotalLlamadasEfectivas = 9 });
            defaultRegistrosMercadeo.Add(new RegistroMercadeo() { usuario = context.Usuarios.Find(4), CantidadVentas = 10, DuracionLlamadaEfectiva = 10, fechaHora = new DateTime(2015, 11, 1), MontoTotalVentas = 300000, PromDuraLlamadasEfectivas = 21, TotalLlamadas = 20, TotalLlamadasEfectivas = 6 });
            defaultRegistrosMercadeo.Add(new RegistroMercadeo() { usuario = context.Usuarios.Find(5), CantidadVentas = 10, DuracionLlamadaEfectiva = 10, fechaHora = new DateTime(2015, 11, 1), MontoTotalVentas = 400000, PromDuraLlamadasEfectivas = 14, TotalLlamadas = 10, TotalLlamadasEfectivas = 3 });

            foreach (RegistroMercadeo registro in defaultRegistrosMercadeo)
                context.RegistrosMercadeo.Add(registro);


            base.Seed(context);

        }
    }
}