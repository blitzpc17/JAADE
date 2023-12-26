using CAPADATOS.ADO.SISTEMA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPALOGICA.LOGICAS.SISTEMA
{
    public class UtileriasLogica:IDisposable
    {
        private ModulosADO contexto;
        
        public UtileriasLogica()
        {
            contexto = new ModulosADO();    
        }

        public void Dispose()
        {
            contexto.Dispose();
        }

        public DateTime ObtenerFechaYHoraServidor()
        {
            return contexto.FechaYHoraActualServidor();
        }




    }
}
