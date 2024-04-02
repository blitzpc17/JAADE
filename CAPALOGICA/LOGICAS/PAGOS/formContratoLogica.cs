using CAPADATOS;
using CAPADATOS.ADO.LOTES;
using CAPADATOS.ADO.PAGOS;
using CAPADATOS.ADO.SISTEMA;
using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPALOGICA.LOGICAS.PAGOS
{
    public class formContratoLogica
    {
        private ClientesADO contextoClientes;
        private SociosADO contextoSocios;
        private ContratoLoteADO contextoContrato;
        private ZonaADO contextoZona;
        private LotesADO contextoLote;
        private EstadoADO contextoEstados;
        private PagoADO contextoPago;

        public CONTRATO ObjContrato;
        public clsClientes ObjCliente;
        public LOTE ObjLote;
        public SOCIOS ObjSocio;
        public clsContratoCliente ObjContratoData;
        public clsObjMontoGracia ObjMontoGraciaData;
        public clsArrendamientoLoteData ObjContratoImpresoData;

        public PAGO ObjPago;

        public List<SOCIOS> LstSociosCliente;
        public List<SOCIOS> LstSociosClienteContrato;//cuando ya esta asignado el cliente al contrato y lote.
        public List<ZONA> LstZonas;
        public List<LOTE> LstLotes;
        public List<LOTE> LstLotesSeleccionados;

        public List<ESTADO> LstEstados;

        public List<clsAGENDACLIENTE> LstAgenda;

        public formContratoLogica()
        {
            contextoClientes = new ClientesADO();
            contextoSocios = new SociosADO();
            contextoContrato = new ContratoLoteADO();
            contextoZona = new ZonaADO();
            contextoLote = new LotesADO(); 
            contextoEstados = new EstadoADO();
            contextoPago = new PagoADO();   
        }
        
        public void ListarCatalogos()
        {
            LstZonas = contextoZona.Listar();
            LstEstados = contextoEstados.Listar().Where(x=>x.Proceso=="CONTRATO").ToList();
        }

        public List<LOTE> ListarLotesXZonaIdEstadoId(int zonaId, int estadoId)
        {
            return contextoLote.ListarLotesXZonaIdEstadoId(zonaId, estadoId);
        }

        public void InstanciarContrato()
        {
            ObjContrato = new CONTRATO();
        }

        public void Guardar()
        {
            
            if (ObjContrato.Id == 0)
            {
                contextoContrato.Insertar(ObjContrato); 
            }          

            contextoContrato.Guardar();

        }

        public void ListarSociosCliente(string claveCliente)
        {
            LstSociosCliente = contextoSocios.ListarClientesSocios(claveCliente);
        }

        public LOTE ObtenerLote(int id)
        {
            return contextoLote.Obtener(id);
        }

        public clsClientes BuscarClientePorClave(string claveCliente)
        {
            return contextoClientes.ObtenerDataClienteClave(claveCliente);
        }

        public LOTE BuscarLotePorClave(string claveLote)
        {
            return contextoLote.ObtenerLoteIdentificador(claveLote);
        }

        public void BuscarContratoFolio(string folioContrato)
        {
            ObjContratoData = contextoContrato.ObtenerContratoClienteFolio(folioContrato);
            if (ObjContratoData != null)
            {
                ObjContrato = contextoContrato.Obtener(ObjContratoData.ContratoId);
            }
          
        }

        public void InstanciarPago(){
            ObjPago = new PAGO();
        }

        public void GuardarPago()
        {
            contextoPago.Insertar(ObjPago);
            contextoPago.Guardar();
        }

        public void GuardarLote()
        {
            contextoLote.Guardar();
        }

        public void CalcularMontoGracia(string folioContrato)
        {
            ObjMontoGraciaData = contextoContrato.CalcularMontoGracia(folioContrato);
        }

        public void ObtenerDatosContratoImpreso(string folioContrato)
        {
            ObjContratoImpresoData = contextoContrato.ObtenerDatosContratoImpreso(folioContrato);
        }

        public void AgregarLoteSeleccionado(int loteId)
        {
           
            if (LstLotesSeleccionados == null)
            {
                LstLotesSeleccionados = new List<LOTE>();
            }

            if (LstLotesSeleccionados.Any(x => x.Id ==  loteId)) return;

            ObjLote = LstLotes.FirstOrDefault(x => x.Id == loteId);
            LstLotesSeleccionados.Add(ObjLote); 
        }

        public bool QuitarLoteSeleccionado(int loteId)
        {
            if (LstLotesSeleccionados == null) return false;
            ObjLote = LstLotesSeleccionados.First(x => x.Id == loteId);
            LstLotesSeleccionados.Remove(ObjLote);

            return true;
        }

        public List<LOTE> ObtenerLotesPorClave( int zonaId, List<string> lstLotes)
        {
            return contextoLote.ObtenerLotesPorClave(zonaId, lstLotes);
        }
    }
}
