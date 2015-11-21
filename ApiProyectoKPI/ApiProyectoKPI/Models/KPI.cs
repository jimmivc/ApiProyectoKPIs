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

        [Required]
        public ParametroKPI Parametro { get; set; }
        public ICollection<DetalleFormula> Formula { get; set; }
        
        public ICollection<Rol> RolesAsignados { get; set; }

        public List<string> calcularResultados(List<RegistroMercadeo> registros, List<Usuario> usuarios){
            List<string> result = new List<string>();

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
                            if (isCalculoCampoUnico())
                            {
                                double datoCampo = getDatoCampo(registro, 0);

                                result.Add(datoCampo.ToString());
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

                                result.Add(aplicarFormula(datos, formula).ToString());
                                result.Add(calcularColorResultado(aplicarFormula(datos, formula)));

                            }
                        }
                    }
                    
                }
            }
            return result;
        }

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

    }
}