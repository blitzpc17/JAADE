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


            return LstImagenes;
        }


    }
}
