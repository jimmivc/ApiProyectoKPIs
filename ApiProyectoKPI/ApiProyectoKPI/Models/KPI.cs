using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace ApiProyectoKPI.Models
{
    public class KPI
    {
        public KPI()
        {
            RolesAsignados = new HashSet<Rol>();
        }

        public int KPIID { get; set; }
        public string DescKpi { get; set; }
        public string Formato { get; set; }
        public double Objetivo { get; set; }
        public bool Estado { get; set; }
        public string Periodicidad { get; set; }

        [Required]
        public ParametroKPI Parametro { get; set; }
        public ICollection<DetalleFormula> Formula { get; set; }
        
        public ICollection<Rol> RolesAsignados { get; set; }
        /// <summary>
        /// calcularResultados
        /// metodo encargado de calcular los resultados kpi para los usuarios que pertenecen a un rol
        /// </summary>
        /// <param name="registros">Registros mercadeo</param>
        /// <returns>List(Of String)</returns>
        /// <remarks><para>Historia de Creación y modificaciones:
        /// <list type="bullet">
        /// <item>Autor.: Jimmi Vila </item>
        /// <item>10/10/2015 - Creación</item>
        /// </list></para></remarks>
        public List<string> calcularResultados(List<RegistroMercadeo> registros, List<Usuario> usuarios){
            List<string> result = new List<string>();

            //agrupar registros
            if (!Periodicidad.Equals("mensual"))
            {
                registros = agruparRegistros(usuarios, registros);
            }

            foreach (Usuario user in usuarios)
            {
                foreach (RegistroMercadeo registro in registros)
                {
                    if (registro.usuario != null)
                    {
                        if (registro.usuario.UsuarioID == user.UsuarioID)
                        {
                            result.Add(DescKpi);
                            result.Add(user.Nombre + " " + user.Apellidos);
                            result.Add(Formato);
                            result.Add(Objetivo.ToString());
                            if (isCalculoCampoUnico())
                            {
                                double datoCampo = getDatoCampo(registro, 0);

                                result.Add(formatoFinal(datoCampo).ToString());
                                result.Add(calcularColorResultado(datoCampo));

                            }
                            else
                            {

                                List<double> datos = new List<double>();
                                List<DetalleFormula> formula = Formula.ToList<DetalleFormula>();

                                for (int i = 0; i < Formula.Count; i++)
                                {
                                    if (formula[i].Tabla != null)
                                    {
                                        //result.Add(getDatoCampo(registro, i).ToString());
                                        datos.Add(getDatoCampo(registro, i));
                                    }
                                    else if (formula[i].Valor != 0)
                                    {
                                        //result.Add(formula[i].Valor.Value.ToString());
                                        datos.Add(formula[i].Valor.Value);
                                    }
                                }

                                result.Add(formatoFinal(aplicarFormula(datos, formula)).ToString());
                                result.Add(calcularColorResultado(aplicarFormula(datos, formula)));

                            }
                        }
                    }
                }
            }
            return result;
        }

        
        private List<RegistroMercadeo> agruparRegistros(List<Usuario> usuarios, List<RegistroMercadeo> registros)
        {
            List<RegistroMercadeo> regGroup = new List<RegistroMercadeo>(); 
            foreach(Usuario user in usuarios){
                RegistroMercadeo registroTotales = new RegistroMercadeo();
                foreach(var reg in registros){
                    if (reg.usuario.UsuarioID == user.UsuarioID)
                    {
                        registroTotales.MontoTotalVentas += reg.MontoTotalVentas;
                        registroTotales.PromDuraLlamadasEfectivas += reg.PromDuraLlamadasEfectivas;
                        registroTotales.CantidadVentas += reg.CantidadVentas;
                        registroTotales.DuracionLlamadaEfectiva += reg.DuracionLlamadaEfectiva;
                        registroTotales.TotalLlamadas += reg.TotalLlamadas;
                        registroTotales.TotalLlamadasEfectivas += reg.TotalLlamadasEfectivas;
                    }
                }
                registroTotales.usuario = user;
                regGroup.Add(registroTotales);
            }
            return regGroup;
        }

        /// <summary>
        /// aplicarFormula
        /// metodo encargado de descomponer la formula y aplicarla
        /// </summary>
        /// <param name="datos">List<double></param>
        /// <param name="formula">List<DetalleFormula></param>
        /// <returns>double</returns>
        /// <remarks><para>Historia de Creación y modificaciones:
        /// <list type="bullet">
        /// <item>Autor.: Jimmi Vila </item>
        /// <item>10/10/2015 - Creación</item>
        /// </list></para></remarks>
        private double aplicarFormula(List<double> datos, List<DetalleFormula> formula)
        {
            int indiceDato = 0;
            double resultado = datos[indiceDato++];
            for (int i = 0; i < formula.Count; i++)
            {
                if(formula[i].TipoDato.Equals("operador")){
                    switch (formula[i].DescCampoOperador)
                    {
                        case "+":
                            resultado += datos[indiceDato];
                            break;
                        case "-":
                            resultado -= datos[indiceDato];
                            break;
                        case "*":
                            resultado *= datos[indiceDato];
                            break;
                        case "/":
                            resultado /= datos[indiceDato];
                            break;
                        default:
                            break;
                    }
                    indiceDato++;
                }
            }
            return resultado;
        }
        /// <summary>
        /// calcularColorResultado
        /// metodo encargado de calcular el color resultado del kpi
        /// </summary>
        /// <param name="resultadosKPI">double</param>
        /// <returns>color resultado del semaforo</returns>
        /// <remarks><para>Historia de Creación y modificaciones:
        /// <list type="bullet">
        /// <item>Autor.: Jimmi Vila </item>
        /// <item>10/10/2015 - Creación</item>
        /// </list></para></remarks>
        private string calcularColorResultado(double resultadoKPI)
        {
            string color;
            if (resultadoKPI >= Parametro.LimiteSuperior)
            {
                color = "verde";
            }
            else if (resultadoKPI < Parametro.LimiteSuperior && resultadoKPI >= Parametro.LimiteInferior)
            {
                color = "amarillo";
            }
            else
            {
                color = "rojo";
            }

            return color;
        }
        /// <summary>
        /// getDatoCampo
        /// metodo encargado de obtener el valor de un campo
        /// </summary>
        /// <param name="registro">RegistroMercadeo</param>
        /// <param name="indiceFormula">int</param>
        /// <returns>double</returns>
        /// <remarks><para>Historia de Creación y modificaciones:
        /// <list type="bullet">
        /// <item>Autor.: Jimmi Vila </item>
        /// <item>10/10/2015 - Creación</item>
        /// </list></para></remarks>
        private double getDatoCampo(RegistroMercadeo registro,int indiceFormula)
        {
            List<DetalleFormula> formula = Formula.ToList<DetalleFormula>();
            double dato;
            switch (formula[indiceFormula].Tabla.ToLower())
            {
                case "llamadas":
                    dato = registro.TotalLlamadas;
                    break;
                case "llamadas efectivas":
                    dato = registro.TotalLlamadasEfectivas;
                    break;
                case "promedio duracion efectivas":
                    dato = registro.PromDuraLlamadasEfectivas;
                    break;
                case "duracion llamadas efectivas":
                    dato = registro.DuracionLlamadaEfectiva;
                    break;
                case "cantidad ventas":
                    dato = registro.CantidadVentas;
                    break;
                case "monto ventas":
                    dato = registro.MontoTotalVentas;
                    break;
                default:
                    dato = 0;
                    break;
            }
            return dato;
        }
        /// <summary>
        /// isCampoUnico
        /// metodo encargado de calcular si el kpi es simple o complejo
        /// </summary>
        /// <returns>bool</returns>
        /// <remarks><para>Historia de Creación y modificaciones:
        /// <list type="bullet">
        /// <item>Autor.: Jimmi Vila </item>
        /// <item>10/10/2015 - Creación</item>
        /// </list></para></remarks>
        private bool isCalculoCampoUnico()
        {
            List<DetalleFormula> formula = Formula.ToList<DetalleFormula>();
            if (Formula.Count == 1 && formula[0].Tabla!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private double formatoFinal(double resultado)
        {
            switch (Formato.ToLower())
            {
                case "123":
                    resultado = Convert.ToInt32(resultado);
                    break;
                case "%":
                    resultado = (resultado * 100) / Objetivo;
                    break;
            }

            return resultado;
        }
    }
}