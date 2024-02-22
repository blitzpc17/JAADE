using CAPADATOS;
using CAPADATOS.ADO.LOTES;
using CAPADATOS.ADO.PAGOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPALOGICA.LOGICAS.PAGOS
{
    public class formImportacionLayoutsLogica
    {

        private ZonaADO contextoZona;
        private ClientesADO contextoCliente;
        private SociosADO contextoSocio;
        private LotesADO contextoLote;
        private ContratoLoteADO contextoClienteLote;
        private PAGO contextoPago;

        public ZONA ObjZona;
        public LOTE ObjLote;
        public CLIENTE ObjCliente;
        public SOCIOS ObjSocio;
        public CLIENTELOTE ObjContrato;
        public PAGO ObjPago;

        public List<ZONA> LstZonasImportadas;


        public formImportacionLayoutsLogica()
        {
            contextoZona = new ZonaADO();
            contextoCliente = new ClientesADO();
            contextoSocio = new SociosADO();    
            contextoLote = new LotesADO();  
            contextoClienteLote = new ContratoLoteADO();    
            contextoPago = new PAGO();  
        }


        #region Zona

        public void InstanciarZona()
        {
            ObjZona = new ZONA();
        }

        public void InstanciarListaImportacionZona()
        {
            LstZonasImportadas = new List<ZONA>();
        }

        public void GuardarZona()
        {
            if (ObjZona.Id == 0)
            {
                contextoZona.Insertar(ObjZona);
            }

            contextoZona.Guardar();

        }

        public ZONA ObtenerZonaNombre(string nombre)
        {
            return contextoZona.ObtenerZonaNombre(nombre);
        }




        #endregion


        #region Cliente
        #endregion

        #region Lotes
        #endregion

        #region Contratos
        #endregion

        #region Pagos
        #endregion




    }
}
