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
    public class formContratoLogica
    {
        private ClientesADO contextoClientes;
        private SociosADO contextoSocios;
        private ContratoLoteADO contextoContrato;
        private ZonaADO contextoZona;
        private LotesADO contextoLote;

        public CLIENTELOTE ObjContrato;
        public clsClientes ObjCliente;
        public LOTE ObjLote;
        public SOCIOS ObjSocio;
        public clsContratoCliente ObjContratoData;

        public List<SOCIOS> LstSociosCliente;
        public List<SOCIOS> LstSociosClienteContrato;//cuando ya esta asignado el cliente al contrato y lote.
        public List<ZONA> LstZonas;
        public formContratoLogica()
        {
            contextoClientes = new ClientesADO();
            contextoSocios = new SociosADO();
            contextoContrato = new ContratoLoteADO();
            contextoZona = new ZonaADO();
            contextoLote = new LotesADO();  
        }
        
        public void ListarCatalogos()
        {
            LstZonas = contextoZona.Listar();
        }

        public void InstanciarContrato()
        {
            ObjContrato = new CLIENTELOTE();
        }

        public void Guardar()
        {
            
            if (ObjContrato.Id == 0)
            {
                contextoContrato.Insertar(ObjContrato);
                contextoContrato.Guardar();
                contextoLote.Obtener(ObjContrato.LOTEId);
                ObjLote.ESTADOId = 4; //asignado
                contextoLote.Guardar(); 
            }
            else
            {
                if (ObjLote.Id != ObjContratoData.LoteId)
                {
                    //modificar estado de lotes
                    ObjLote.ESTADOId = 4;//NuevoLote
                    contextoLote.Guardar();
                    ObjLote = contextoLote.Obtener(ObjContratoData.LoteId);
                    contextoLote.Guardar();
                }
                contextoContrato.Guardar();
            }
            
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
    }
}
