using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRESENTACION.UTILERIAS
{
    public static class Enumeraciones
    {

        public enum VariablesGlobales
        {
            ThemeConfiguracion,
            DomicilioJaade,
            ConsecutivoClientes,
            ConsecutivoPagos,
            CredencialesFtp,
            CredencialesTwilio,
            CredencialesCorreo
        }

        public static Dictionary<string, Image> ListaImagenes()
        {
            Dictionary<string, Image> LstImagenes = new Dictionary<string, Image>();

            LstImagenes.Add("categoria", Properties.Resources.categoria);
            LstImagenes.Add("reportes", Properties.Resources.reporte);
            LstImagenes.Add("consulta", Properties.Resources.registro);
            LstImagenes.Add("configuracion", Properties.Resources.registro);
            LstImagenes.Add("pagos", Properties.Resources.pagar);
            LstImagenes.Add("seguridad", Properties.Resources._lock);
            LstImagenes.Add("permiso", Properties.Resources.permiso);
            LstImagenes.Add("asignacion", Properties.Resources.asignar);


            return LstImagenes;
        }

        public enum Procesos
        {
            USUARIO,
            CLIENTE,
            LOTE
        }

        public enum EstadosProcesoLote
        {
            LIBRE,
            ASIGNADO,
            VENDIDO
        }
        public enum EstadosProcesoUsuario
        {
            ACTIVO,
            INACTIVO
        }
        public enum EstadosProcesoCliente
        {
            ACTIVO,
            INACTIVO
        }


    }
}
