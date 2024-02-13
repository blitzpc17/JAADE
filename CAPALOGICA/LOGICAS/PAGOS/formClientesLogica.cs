using CAPADATOS;
using CAPADATOS.ADO.PAGOS;
using CAPADATOS.ADO.SISTEMA;
using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CAPALOGICA.LOGICAS.PAGOS
{
    public class formClientesLogica
    {
        private ClientesADO contextoClientes;
        private EstadoADO contextoEstado;
        private PersonaADO contextoPersona;
        private SociosADO contextoSocios;
        private ClienteSocioADO contextoClienteSocio;
        private ContactoClienteADO contextoContacto;
        private Persona_AgendaADO contextoPersonaAgenda;

        public clsClientesImportacion ObjImportacion;
        public CLIENTE ObjCliente;
        public PERSONA ObjPersona;
        public SOCIOS ObjSocios;
        public AGENDA ObjAgenda;
        public PERSONA_AGENDA ObjPersonaAgenda;
        public CLIENTES_SOCIOS ObjClienteSocio;
        public List<ESTADO> LstEstado;
        public clsClientes ObjClienteData;
        public List<clsClientes> LstClientes;
        public List<clsClientes> LstClientesAux;
        public List<SOCIOS> LstClientesSocios;

        //import list
        public List<clsClientesImportacion> LstImportacion;

        public int index = -1;
        public int indexAux = -1;
        public int Column = 0;

        public List<clsAGENDACLIENTE> LstAgendasCliente;

        public List<KeyValuePair<string, int>> LstTipos;

        public formClientesLogica()
        {
            contextoClientes = new ClientesADO();
            contextoEstado = new EstadoADO();
            contextoPersona = new PersonaADO();
            
            contextoContacto = new ContactoClienteADO();
            contextoPersonaAgenda = new Persona_AgendaADO();

            contextoSocios = new SociosADO();
            contextoClienteSocio = new ClienteSocioADO();
        }
        public void InstanciarCliente()
        {
            ObjCliente = new CLIENTE();
        }
        public void InstanciarObjImportacion()
        {
            ObjImportacion = new clsClientesImportacion();
        }
        public void ListarCatalogos()
        {
            LstEstado = contextoEstado.Listar().Where(x => x.Proceso == "CLIENTE").ToList();

            LstTipos = new List<KeyValuePair<string, int>>();

            LstTipos.Add(new KeyValuePair<string, int>("TELEFONO", 1));
            LstTipos.Add(new KeyValuePair<string, int>("DIRECCION", 2));
            LstTipos.Add(new KeyValuePair<string, int>("CORREO ELECTRONICO", 3));
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

        public void InstanciarPersona()
        {
            ObjPersona = new PERSONA();
        }      

        public void InstanciarListasImportacion()
        {
            LstImportacion = new List<clsClientesImportacion>();            
            
        }
        public CLIENTE ObtenerCliente(int id)
        {
            return contextoClientes.Obtener(id);
        }
        public PERSONA ObtenerPersona(int idPersona)
        {
            return contextoPersona.Obtener(idPersona);
        }   
        public void ListarClientes()
        {
            LstClientesAux = contextoClientes.ListarClientes();
        }

        public clsClientes ObtenerDataCliente(int id)
        {
            return contextoClientes.ObtenerDataCliente(id);
        }
        public void BuscarClientePorClave(string clave)
        {
            ObjClienteData = contextoClientes.ObtenerDataClienteClave(clave);
            ObjCliente = ObtenerCliente(ObjClienteData.Id);
            ObjPersona = contextoPersona.Obtener(ObjCliente.PERSONAId);
        }
        public void InstanciarSocio()
        {
            ObjSocios = new SOCIOS();
            ObjClienteSocio = new CLIENTES_SOCIOS();
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
            if(ObjAgenda.Id == 0)
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

        public void ListarClientesSocio(string clave)
        {
            LstClientesSocios = contextoSocios.ListarClientesSocios(clave);
        }

        public void ListarAgendaContacto(string claveCliente)
        {
            LstAgendasCliente = contextoPersonaAgenda.ListarAgendaContactoCliente(claveCliente);
        }

        public void ObtenerContacto(int contactoId)
        {
            ObjAgenda = contextoContacto.Obtener(contactoId);
            ObjPersonaAgenda = contextoPersonaAgenda.Obtener(ObjAgenda.Id);
        }

        public void EliminarContacto()
        {
            contextoPersonaAgenda.Eliminar(ObjPersonaAgenda);
            contextoPersonaAgenda.Guardar();
            contextoContacto.Eliminar(ObjAgenda);
            contextoContacto.Guardar();
        }

        public void ObtenerSocio(int socioId)
        {
            ObjSocios = contextoSocios.Obtener(socioId);
            ObjClienteSocio = contextoClienteSocio.Obtener(ObjSocios.Id);
        }

        public void EliminarSocio()
        {
            contextoSocios.Eliminar(ObjSocios);
            contextoSocios.Guardar();
            contextoClienteSocio.Eliminar(ObjClienteSocio);
            contextoClienteSocio.Guardar();
        }

        public SOCIOS BuscarSocioPorNombre(string nombreSocio, int clienteId)
        {
            return contextoSocios.BuscarSociosPorNombreClienteId(clienteId, nombreSocio);
        }
    }
}
