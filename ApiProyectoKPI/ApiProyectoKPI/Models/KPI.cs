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

        public String calcularResultados(List<RegistroMercadeo> registros, List<Usuario> usuarios){
            string result = "";

            foreach(Usuario user in usuarios){
               foreach (RegistroMercadeo registro in registros)
               {
                   if(registro.usuario.UsuarioID == user.UsuarioID)
                    if (isCalculoCampoUnico())
                    {
                        double datoCampo = getDatoCampo(registro,0);
                        
                        result += datoCampo.ToString();
                        result += calcularColorResultado(datoCampo);
                        result += "/n";
                    }
                    else
                    {
                        
                        List<double> datos = new List<double>();
                        List<DetalleFormula> formula = Formula.ToList<DetalleFormula>();

                        for (int i = 0; i < Formula.Count; i++)
                        {
                            if (formula[i].Tabla != null)
                            {

                                result += getDatoCampo(registro,i);
                                datos.Add(getDatoCampo(registro, i));
                            }
                            else if(formula[i].Valor !=0)
                            {
                                result += formula[i].Valor.Value;
                                datos.Add(formula[i].Valor.Value);
                            }
                        }
                        result += "resultado = ";
                        result += aplicarFormula(datos,formula);
                    }
                }
            }
            return result;
        }

        private double aplicarFormula(List<double> datos, List<DetalleFormula> formula)
        {
            double resultado = 0;
            for (int i = 0; i < formula.Count-2; i++)
            {
                if(formula[i].TipoDato.Equals("operador")){
                    switch (formula[i].DescCampoOperador)
                    {
                        case "+":
                            resultado += datos[i-1] + datos[i];
                            break;
                        case "-":
                            resultado += datos[i-1] - datos[i];
                            break;
                        case "*":
                            resultado += datos[i-1] * datos[i];
                            break;
                        case "/":
                            resultado += datos[i-1] / datos[i];
                            break;
                        default:
                            break;
                    }
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
                case "totalllamadas":
                    dato = registro.TotalLlamadas;
                    break;
                case "totalllamadasefectivas":
                    dato = registro.TotalLlamadasEfectivas;
                    break;
                case "promdurallamadasefectivas":
                    dato = registro.PromDuraLlamadasEfectivas;
                    break;
                case "duracionllamadaefectiva":
                    dato = registro.DuracionLlamadaEfectiva;
                    break;
                case "cantidadventas":
                    dato = registro.CantidadVentas;
                    break;
                case "montototalventas":
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