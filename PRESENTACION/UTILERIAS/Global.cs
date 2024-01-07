using CAPADATOS;
using CAPADATOS.ADO.SISTEMA;
using CAPADATOS.Entidades;
using CAPALOGICA.LOGICAS.SISTEMA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRESENTACION.UTILERIAS
{
    public static class Global
    {

        public static USUARIO ObjUsuario;

        public static bool EsValorEntero(string valor)
        {
            return int.TryParse(valor, out _);
        }

        public static bool EsValorDecimal(string valor)
        {
            return decimal.TryParse(valor, out _);
        }

        public static DateTime FechaServidor()
        {
            using (var contexto = new UtileriasLogica())
            {
                return contexto.ObtenerFechaYHoraServidor();
            }
        }

        public static void CredencialesSesionAcceso(USUARIO obj)
        {
            ObjUsuario = obj;
        }

        public static dynamic DevulveVariableGlobal(Enumeraciones.VariablesGlobales variable)
        {
            using (var contexto = new VariableGlobalADO())
            {
                return contexto.Obtener(variable.ToString());                
            }
        } 

        public static decimal CalcularPagoMensualRestante(decimal restante, decimal noPagos)
        {
            return restante / noPagos;
        }

        public static string MesALetra(int numeroMes)
        {
            switch (numeroMes)
            {
                case 1: return "enero";
                case 2: return "febrero";
                case 3: return "marzo";
                case 4: return "abril";
                case 5: return "mayo";
                case 6: return "junio";
                case 7: return "julio";
                case 8: return "agosto";
                case 9: return "septiembre";
                case 10: return "octubre";
                case 11: return "noviembre";
                case 12: return "diciembre";
                default: return "Mes no válido";
            }
        }

        public static string DigitoAnio(DateTime fecha)
        {
            return (fecha.Year % 100).ToString();
        }

        public static string ArmarDomicilioCliente(clsClientes obj)
        {
            string Calle = obj.Calle,
                NoExt = obj.NoExt,
                NoInt = obj.NoInt,
                Colonia = obj.Colonia,
                Localidad = obj.Localidad,
                CodigoPostal = obj.CodigoPostal,
                Municipio = "",
                Estado = "";

            return Calle + " NO. " + NoExt + (string.IsNullOrEmpty(obj.NoInt) ? "" : " INT. " + obj.NoInt) + " COL. " + obj.Colonia + ". " + (Municipio.Equals(Localidad) ? (Localidad+", "+Localidad) : (Localidad+", "+Municipio)) + ", " + Estado;


        }

        public static bool GuardarExcepcion(Exception ex, string formularioNombre)
        {
            try
            {
                using (var contexto = new ExcepcionADO())
                {
                    contexto.Insertar(new EXCEPCION
                    {
                        Fecha = FechaServidor(),
                        Formulario = formularioNombre,
                        Resumen = ex.Message,
                        Detalle = ex.StackTrace,
                        USUARIOId = ObjUsuario.Id
                    }) ;
                    contexto.Guardar();
                    return true;
                }
                    
            }
            catch(Exception)
            {
                //bloqueo en la bd
                return false;
            }
            
        }


        
    }
}
