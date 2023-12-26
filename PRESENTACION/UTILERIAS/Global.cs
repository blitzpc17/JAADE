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

        public static bool EsValorEntero(string valor)
        {
            return int.TryParse(valor, out _);
        }

        public static DateTime FechaServidor()
        {
            using (var contexto = new UtileriasLogica())
            {
                return contexto.ObtenerFechaYHoraServidor();
            }
        }


        
    }
}
