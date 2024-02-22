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
            CredencialesCorreo,
            ConsecutivoContratos
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
            LstImagenes.Add("usuario", Properties.Resources.persona);


            return LstImagenes;
        }

        public enum ProcesoFolio
        {            
            PAGO,
            CLIENTE,
            CONTRATO
        }

        public enum Procesos
        {
            USUARIO = 1,
            CLIENTE =2,
            LOTE = 3
        }

        public enum EstadosProcesoLote
        {
            LIBRE = 3,
            ASIGNADO = 4,
            VENDIDO = 5
        }
        public enum EstadosProcesoUsuario
        {
            ACTIVO = 1,
            INACTIVO = 2
        }
        public enum EstadosProcesoCliente
        {
            ACTIVO = 6,
            INACTIVO = 7
        }

        public enum EstadosProcesoContratos
        {
            VIGENTE = 8,
            ATRASADO = 9,
            TERMINADO = 10,
            RECISION = 11,
            REUBICADO = 12,
            CANCELADO = 13
        }


    }
}
