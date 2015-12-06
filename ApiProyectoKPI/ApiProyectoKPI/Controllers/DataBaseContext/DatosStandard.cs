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
            IList<TipoCurso> defaultTiposCurso = new List<TipoCurso>();
            defaultTiposCurso.Add(new TipoCurso () { NombreTipoCurso = "Libre" });
            defaultTiposCurso.Add(new TipoCurso () { NombreTipoCurso = "Acti" });
            defaultTiposCurso.Add(new TipoCurso() { NombreTipoCurso = "Carrera" });
            
            IList<Rol> defaultRols = new List<Rol>();
            defaultRols.Add(new Rol() { Nombre = "Administrador" });
            defaultRols.Add(new Rol() { Nombre = "Soporte" });
            defaultRols.Add(new Rol() { Nombre = "Mercadeo" });
            defaultRols.Add(new Rol() { Nombre = "Profesor" });
            defaultRols.Add(new Rol() { Nombre = "Estudiante" });

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

            foreach (Rol rol in defaultRols) { 
                context.Rols.Add(rol);
            }
            context.Usuarios.Add(new Usuario() { Nombre = "Jimmi", Apellidos = "Vila", Cedula = 160400, Correo = "jvilac@ucenfotec.ac.cr", Contrasena = "tXFOeepeTOFiXdv5UUVUBA==", IsActivo = true, Rol = context.Rols.Find(1) });
            context.Usuarios.Add(new Usuario() { Nombre = "Christian", Apellidos = "Ulloa", Cedula = 109150113, Correo = "culloat@ucenfotec.ac.cr", Contrasena = "tXFOeepeTOFiXdv5UUVUBA==", IsActivo = true, Rol = context.Rols.Find(1) });
            context.Usuarios.Add(new Usuario() { Nombre = "Hernán", Apellidos = "Sáenz", Cedula = 99999999, Correo = "gsaenzp@ucenfotec.ac.cr", Contrasena = "tXFOeepeTOFiXdv5UUVUBA==", IsActivo = true, Rol = context.Rols.Find(1) });
            context.Usuarios.Add(new Usuario() { Nombre = "Alvaro", Apellidos = "Cordero", Cedula = 99999999, Correo = "acordero@ucenfotec.ac.cr", Contrasena = "tXFOeepeTOFiXdv5UUVUBA==", IsActivo = true, Rol = context.Rols.Find(1) });
            context.Usuarios.Add(new Usuario() { Nombre = "Alvaro", Apellidos = "Cordero", Cedula = 99999999, Correo = "aguevara@ucenfotec.ac.cr", Contrasena = "tXFOeepeTOFiXdv5UUVUBA==", IsActivo = true, Rol = context.Rols.Find(1) });
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
            foreach (TipoCurso tipoCurso in defaultTiposCurso)
            {
                context.TipoCursoes.Add(tipoCurso);
            }
            base.Seed(context);
        }
    }
}