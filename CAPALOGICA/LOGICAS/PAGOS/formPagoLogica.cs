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
        
  //      public clsPagoData ObjPagoData;
        public PAGO ObjPago;
        public clsContratoCliente ObjContratoData;
        public CONTRATO ObjContrato;

        public clsInformacionContratoPago ObjInformacionPago;
        public clsTicketEncabezado ObjEncabezadoTicket;
        public List<clsTicketPartida> LstPartidasTicket;
        public clsTicketPago ObjTicket;

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

        public void GuardarContrato()
        {
            contextoLote.Guardar();
        }

      /*  public void BuscarPagoFolio(string folioContrato)
        {
            ObjPagoData = contextoPago.ObtenerPagoClienteFolio(folioContrato);
            if (ObjPagoData != null)
            {
                ObjPago = contextoPago.Obtener(ObjPagoData.PagoId);
                ObjContratoData = contextoContrato.ObtenerContratoClienteFolio(ObjPagoData.FolioContrato);
            }
        }*/

        public void BuscarContratoFolio(string folioContrato)
        {
            ObjContratoData = contextoContrato.ObtenerContratoClienteFolio(folioContrato);
            if (ObjContratoData != null)
            {
                ObjContrato = contextoContrato.Obtener(ObjContratoData.ContratoId);
                ObtenerInformacionPago(folioContrato);
            }
            
        }

        public void BuscarContratoId(int ContratoId)
        {
            ObjContratoData = contextoContrato.ObtenerContratoClienteXId(ContratoId);
            if (ObjContratoData != null)
            {
                ObjContrato = contextoContrato.Obtener(ObjContratoData.ContratoId);
            }

        }

        public void ObtenerInformacionPago(string folio)
        {
            ObjInformacionPago = contextoPago.ObtenerInformacionPago(folio);
        }

        public void ObtenerPago(int id)
        {
            ObjPago = contextoPago.Obtener(id);
        }

        public void ObtenerPagFolioo(string folio)
        {
            ObjPago = contextoPago.ObtenerXFolio(folio);
        }

        //ticket
        public void InstanciarObjTicket()
        {
            ObjTicket = new clsTicketPago();
        }

        public void InstanciarEncabezadoTicket()
        {
            ObjEncabezadoTicket = new clsTicketEncabezado();
        }

        public void ObtenerPartidasPagoContrato(string noReferencia)
        {
            LstPartidasTicket = contextoPago.ObtenerPartidasPagoContrato(noReferencia);
        }      

    }
}
