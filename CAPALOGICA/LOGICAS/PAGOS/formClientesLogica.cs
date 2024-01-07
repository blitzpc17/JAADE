using CAPADATOS;
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
    public class formClientesLogica
    {
        private ClientesADO contextoClientes;
        private EstadoADO contextoEstado;
        private PersonaADO contextoPersona;


        public CLIENTE ObjCliente;
        public PERSONA ObjPersona;
        public List<ESTADO> LstEstado;
        public clsClientes ObjClienteData;
        public List<clsClientes> LstClientes;
        public List<clsClientes> LstClientesAux;

        public int index = -1;
        public int indexAux = -1;
        public int Column = 0;


        public formClientesLogica()
        {
            contextoClientes = new ClientesADO();
            contextoEstado = new EstadoADO();
            contextoPersona = new PersonaADO();
        }

        public void InstanciarCliente()
        {
            ObjCliente = new CLIENTE();
        }

        public void InstanciarPersona()
        {
            ObjPersona = new PERSONA();
        }

        public void ListarCatalogos()
        {
            LstEstado = contextoEstado.Listar().Where(x => x.Proceso == "CLIENTE").ToList();
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

        public CLIENTE ObtenerCliente(int id)
        {
            return contextoClientes.Obtener(id);
        }
        public PERSONA ObtenerPersona(int idPersona)
        {
            return contextoPersona.Obtener(idPersona);
        }

        public bool Filtrar(int column, string termino)
        {
            if (LstClientesAux == null) LstClientesAux = LstClientes;

            switch (column)
            {
                case 1:
                    index = LstClientesAux.FindIndex(x => x.Clave.StartsWith(termino));
                    break;
                case 2:
                    index = LstClientesAux.FindIndex(x => x.Cliente.ToString().StartsWith(termino));
                    break;
                case 11:
                    index = LstClientesAux.FindIndex(x => x.Colonia.StartsWith(termino));
                    break;
                case 12:
                    index = LstClientesAux.FindIndex(x => x.Localidad.StartsWith(termino));
                    break;
                case 13:
                    index = LstClientesAux.FindIndex(x => x.Municipio.StartsWith(termino));
                    break;
                case 14:
                    index = LstClientesAux.FindIndex(x => x.EntidadFederativa.StartsWith(termino));
                    break;
                case 17:
                    index = LstClientesAux.FindIndex(x => x.Estado.StartsWith(termino));
                    break;


                default:
                    index = -1;
                    break;

            }

            return (index >= 0);

        }

        public void Ordenar(int column)
        {
            switch (column)
            {

                case 1:
                    LstClientesAux = LstClientes.OrderBy(x => x.Clave).ThenBy(x => x.Cliente).ThenBy(x => x.Colonia).ThenBy(x=>x.Localidad).ThenBy(x => x.Municipio).ThenBy(x => x.EntidadFederativa).ThenBy(x => x.Estado).ToList();
                    break;
                case 2:
                    LstClientesAux = LstClientes.OrderBy(x => x.Cliente).ThenBy(x => x.Colonia).ThenBy(x => x.Localidad).ThenBy(x => x.Municipio).ThenBy(x => x.EntidadFederativa).ThenBy(x => x.Estado).ThenBy(x => x.Clave).ToList();
                    break;
                case 11:
                    LstClientesAux = LstClientes.OrderBy(x => x.Colonia).ThenBy(x => x.Localidad).ThenBy(x => x.Municipio).ThenBy(x => x.EntidadFederativa).ThenBy(x => x.Estado).ThenBy(x => x.Clave).ThenBy(x => x.Cliente).ToList();
                    break;
                case 12:
                    LstClientesAux = LstClientes.OrderBy(x => x.Localidad).ThenBy(x=>x.Municipio).ThenBy(x=>x.EntidadFederativa).ThenBy(x => x.Estado).ThenBy(x => x.Clave).ThenBy(x => x.Cliente).ThenBy(x=>x.Colonia).ToList();
                    break;
                case 13:
                    LstClientesAux = LstClientes.OrderBy(x => x.Municipio).ThenBy(x => x.EntidadFederativa).ThenBy(x => x.Estado).ThenBy(x => x.Clave).ThenBy(x => x.Cliente).ThenBy(x=>x.Colonia).ThenBy(x=>x.Localidad).ToList();
                    break;
                case 14:
                    LstClientesAux = LstClientes.OrderBy(x => x.EntidadFederativa).ThenBy(x => x.Estado).ThenBy(x => x.Clave).ThenBy(x => x.Cliente).ThenBy(x=>x.Colonia).ThenBy(x=>x.Localidad).ThenBy(x=>x.Municipio).ToList();
                    break;

                case 17:
                    LstClientesAux = LstClientes.OrderBy(x => x.Estado).ThenBy(x => x.Clave).ThenBy(x => x.Cliente).ThenBy(x => x.Colonia).ThenBy(x => x.Localidad).ToList();
                    break;

                default:
                    LstClientesAux = LstClientes.OrderBy(x => x.Clave).ThenBy(x => x.Cliente).ThenBy(x => x.Colonia).ThenBy(x => x.Localidad).ThenBy(x => x.Municipio).ThenBy(x => x.EntidadFederativa).ThenBy(x => x.Estado).ToList();
                    break;

            }
        }


        public clsClientes ObtenerDataCliente(int id)
        {
            return contextoClientes.ObtenerDataCliente(id);
        }

        public void ListarUsuarios()
        {
            LstClientes = contextoClientes.ListarClientes();
            LstClientesAux = LstClientes;
        }


        public void GenerarClaveCliente()
        {

        }



    }
}
