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
    public class formImportacionLayoutsLogica
    {

        private ZonaADO contextoZona;

        private ClientesADO contextoClientes;
        private ContactoClienteADO contextoContacto;
        private Persona_AgendaADO contextoPersonaAgenda;
        private PersonaADO contextoPersona;
        private SociosADO contextoSocios;
        private ClienteSocioADO contextoClienteSocio;

        private LotesADO contextoLote;

        private ContratoLoteADO contextoClienteLote;

        private PagoADO contextoPago;

        public ZONA ObjZona;
        public LOTE ObjLote;
        public clsLoteImportacion ObjLoteImportacion;
        public PERSONA ObjPersona;
        public CLIENTE ObjCliente;
        public AGENDA ObjAgenda;
        public PERSONA_AGENDA ObjPersonaAgenda;
        public clsClientes ObjClienteData;
        public clsClientesImportacion ObjClienteImportacion;
        public CLIENTES_SOCIOS ObjClienteSocio;
        public SOCIOS ObjSocios;
        public CONTRATO ObjContrato;
        public clsCONTRATOIMPORTACION ObjContratoImportacion;
        public PAGO ObjPago;
        public CONTRATO ObjClienteLote;

        public List<ZONA> LstZonasImportadas;

        public List<clsClientesImportacion> LstImportacionCliente;
        public List<clsClientes> LstClientesExistentes;

        public List<clsLoteImportacion> LstLotesImportados;
        public List<ZONA> LstZonasAux;
        public ZONA ObjZonaAux;

        public List<clsCONTRATOIMPORTACION> LstContratosImpotacion;

        public List<clsPagoImportacion> LstPagosImportacion;
        public clsPagoImportacion ObjPagoImportacion;

        public formImportacionLayoutsLogica()
        {
            contextoZona = new ZonaADO();

            contextoClientes = new ClientesADO();
            contextoSocios = new SociosADO();
            contextoPersonaAgenda = new Persona_AgendaADO();
            contextoClienteSocio = new ClienteSocioADO();
            contextoContacto = new ContactoClienteADO();
            contextoPersona = new PersonaADO();

            contextoLote = new LotesADO();

            contextoClienteLote = new ContratoLoteADO();

            contextoPago = new PagoADO();   
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

        public void InstanciarListaImportacionClientes()
        {
            LstImportacionCliente = new List<clsClientesImportacion>();
        }

        public void InstanciarObjClienteImportacion()
        {
            ObjClienteImportacion = new clsClientesImportacion();
        }

        public void ListarClientes()
        {
            LstClientesExistentes = contextoClientes.ListarClientes();
        }

        public void InstanciarPersona()
        {
            ObjPersona = new PERSONA();
        }

        public void InstanciarCliente()
        {
            ObjCliente = new CLIENTE();
        }

        public void InstanciarSocio()
        {
            ObjSocios = new SOCIOS();
            ObjClienteSocio = new CLIENTES_SOCIOS();
        }

        public CLIENTE ObtenerCliente(int id)
        {
            return contextoClientes.Obtener(id);
        }
        public PERSONA ObtenerPersona(int idPersona)
        {
            return contextoPersona.Obtener(idPersona);
        }

        public void Guardar()
        {
            if (ObjCliente.Id == 0)
            {
                contextoPersona.Insertar(ObjPersona);
                contextoPersona.Guardar();
                ObjCliente.PERSONAId = ObjPersona.Id;
                contextoClientes.Insertar(ObjCliente);
                contextoClientes.Guardar();
            }
            else
            {
                contextoPersona.Guardar();
                contextoClientes.Guardar();
            }

        }

        public SOCIOS BuscarSocioPorNombre(string nombreSocio, int clienteId)
        {
            return contextoSocios.BuscarSociosPorNombreClienteId(clienteId, nombreSocio);
        }

        public void GuardarSocio()
        {
            if (ObjSocios.Id == 0)
            {
                contextoSocios.Insertar(ObjSocios);
                contextoSocios.Guardar();
                ObjClienteSocio.CLIENTEId = ObjCliente.Id;
                ObjClienteSocio.SOCIOSId = ObjSocios.Id;
                contextoClienteSocio.Insertar(ObjClienteSocio);
                contextoClienteSocio.Guardar();
            }
            else
            {
                contextoSocios.Guardar();
            }
        }

        public void InstanciarContactoAgenda()
        {
            ObjAgenda = new AGENDA();
            ObjPersonaAgenda = new PERSONA_AGENDA();
        }

        public void GuardarContactoAgenda()
        {
            if (ObjAgenda.Id == 0)
            {
                contextoContacto.Insertar(ObjAgenda);
                contextoContacto.Guardar();
                ObjPersonaAgenda.AGENDAId = ObjAgenda.Id;
                ObjPersonaAgenda.PERSONAId = ObjPersona.Id;
                contextoPersonaAgenda.Insertar(ObjPersonaAgenda);
                contextoPersonaAgenda.Guardar();
            }
            else
            {
                contextoContacto.Guardar();
            }
        }

        #endregion

        #region Lotes

        public void ListarZonas()
        {
            LstZonasAux = contextoZona.Listar();
        }
        public void InstanciarListaImportacionLote()
        {
            LstLotesImportados = new List<clsLoteImportacion>();
        }

        public void InstanciarLoteImportacion()
        {
            ObjLoteImportacion = new clsLoteImportacion();
        }

        public void InstanciarLote()
        {
            ObjLote = new LOTE();
        }

        public void GuardarLote()
        {
            if(ObjLote.Id == 0)
            {
                contextoLote.Insertar(ObjLote);
            }            
            contextoLote.Guardar();
        }

        public string ObtenerIdentificadorUltimoLote(int zonaId)
        {
            return contextoLote.ObtenerIdentificadorUltimoLote(zonaId);
        }



        #endregion

        #region Contratos

            public void InstanciarListaImportacionContratos()
            {
                LstContratosImpotacion = new List<clsCONTRATOIMPORTACION>();
            }
            public void InstanciarContratoImportacion()
            {
                ObjContratoImportacion = new clsCONTRATOIMPORTACION();
            }

            public void ObtenerClienteXClave(string claveCliente)
            {
                ObjCliente = contextoClientes.ObtenerXClave(claveCliente);
            }

            public void ObtenerLoteXIdentidicador(string zona, string identificadorLote)
            {
                ObjLote = contextoLote.ObtenerXIdentificador(zona, identificadorLote);
            }

            public void ObtenerSocioXNombre(int clienteId, string nombre)
            {
                ObjSocios = contextoSocios.ObtenerSocioXNombres(clienteId, nombre);
            }

            public void GuardarContrato()
            {            
                contextoClienteLote.Insertar(ObjContrato);
                contextoClienteLote.Guardar();
            }          

            public void InstanciarContrato()
            {
                ObjContrato = new CONTRATO();
            }



        #endregion

        #region Pagos

        public void InstanciarPago()
        {
            ObjPago = new PAGO();
        }

        public void GuardarPago()
        {
            contextoPago.Insertar(ObjPago);
            contextoPago.Guardar();
        }

        public void InstanciarListaPago()
        {
            LstPagosImportacion = new List<clsPagoImportacion>();
        }

        public void InstanciarPagoImportacion()
        {
            ObjPagoImportacion = new clsPagoImportacion();
        }

        public void ObtenerContrato(string contrato)
        {
            ObjContrato = contextoClienteLote.ObtenerContratoXFolio(contrato);
        }
        #endregion




    }
}
