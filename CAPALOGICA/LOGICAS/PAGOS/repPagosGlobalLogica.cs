using CAPADATOS;
using CAPADATOS.ADO.LOTES;
using CAPADATOS.ADO.PAGOS;
using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPALOGICA.LOGICAS.PAGOS
{
    public class repPagosGlobalLogica
    {
        private ZonaADO contextoZonas;
        private PagoADO contextoPago;

        public List<ZONA> LstZonas;
        public List<clsPagoReciboEncabezado> LstEncabezados;
        public List<clsPagoReciboPartida> LstPartidas;

        public List<KeyValuePair<string, int>> LstExportados;

        public repPagosGlobalLogica()
        {
            contextoZonas = new ZonaADO();
            contextoPago = new PagoADO();
        }

        public void ListarZonas()
        {
            LstZonas = contextoZonas.Listar();
        }

        public void ListarEncabezadosPagoXZona(int? ZonaId=null)
        {
            LstEncabezados = contextoPago.ListarEncabezadosPagoXZona(ZonaId);
        }

        public void ListarPartidasPagoXZona(int? ZonaId = null)
        {
            LstPartidas = contextoPago.ListarPartidasPagoXZona(ZonaId);
        }

        public void InstanciarLstExportados()
        {
            LstExportados = new List<KeyValuePair<string, int>>();
        }




    }
}
