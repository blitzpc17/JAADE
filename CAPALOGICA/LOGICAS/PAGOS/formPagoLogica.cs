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
    public class formPagoLogica
    {
        private PagoADO contextoPago;
        private LotesADO contextoLote;
        private ContratoLoteADO contextoContrato;
        
        public clsPagoData ObjPagoData;
        public PAGO ObjPago;
        public clsContratoCliente ObjContratoData;
        
        public formPagoLogica()
        {
            contextoPago = new PagoADO();
            contextoLote = new LotesADO();
            contextoContrato = new ContratoLoteADO();
        }

        public void InstanciarPago()
        {
            ObjPago = new PAGO();
        }

        public void Guardar()
        {
            if (ObjPago.Id == 0)
            {
                contextoPago.Insertar(ObjPago);  
            }
            contextoPago.Guardar();
        }

        public void BuscarPagoFolio(string folioContrato)
        {
            ObjPagoData = contextoPago.ObtenerPagoClienteFolio(folioContrato);
            if (ObjPagoData != null)
            {
                ObjPago = contextoPago.Obtener(ObjPagoData.PagoId);
                ObjContratoData = contextoContrato.ObtenerContratoClienteFolio(ObjPagoData.FolioContrato);
            }
        }

        public void BuscarContratoFolio(string folioContrato)
        {
            ObjContratoData = contextoContrato.ObtenerContratoClienteFolio(folioContrato); 
        }

        




    }
}
