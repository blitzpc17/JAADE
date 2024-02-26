using CAPADATOS.ADO.PAGOS;
using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPALOGICA.LOGICAS.PAGOS
{
    public class repCorteCajaLogica
    {
        private PagoADO contexto;

        public List<clsRepCorteCaja> LstRegistros;

        public repCorteCajaLogica()
        {
            contexto = new PagoADO();
        }

        public void ListarPagosPorFechaEmision(DateTime fechaInicio, DateTime fechaFin)
        {
            LstRegistros = contexto.ListarPagosPorFechaEmision(fechaInicio, fechaFin);
        }




    }
}
