using CAPADATOS.ADO.PAGOS;
using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPALOGICA.LOGICAS.PAGOS
{
    public class busClientesLogica
    {
        private ClientesADO contexto;
        public List<clsClientes> LstClientes;
        public List<clsClientes> LstClientesAux;

        public int index = -1;
        public int indexAux = -1;
        public int Column = 0;

        public busClientesLogica()
        {
            contexto = new ClientesADO();
        }

        public void ListarRegistros()
        {
            LstClientes = contexto.ListarClientes();
        }

        public clsClientes ObtenerRegistro(int id)
        {
            return contexto.ObtenerDataCliente(id);
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
                    LstClientesAux = LstClientes.OrderBy(x => x.Clave).ThenBy(x => x.Cliente).ThenBy(x => x.Colonia).ThenBy(x => x.Localidad).ThenBy(x => x.Municipio).ThenBy(x => x.EntidadFederativa).ThenBy(x => x.Estado).ToList();
                    break;
                case 2:
                    LstClientesAux = LstClientes.OrderBy(x => x.Cliente).ThenBy(x => x.Colonia).ThenBy(x => x.Localidad).ThenBy(x => x.Municipio).ThenBy(x => x.EntidadFederativa).ThenBy(x => x.Estado).ThenBy(x => x.Clave).ToList();
                    break;
                case 11:
                    LstClientesAux = LstClientes.OrderBy(x => x.Colonia).ThenBy(x => x.Localidad).ThenBy(x => x.Municipio).ThenBy(x => x.EntidadFederativa).ThenBy(x => x.Estado).ThenBy(x => x.Clave).ThenBy(x => x.Cliente).ToList();
                    break;
                case 12:
                    LstClientesAux = LstClientes.OrderBy(x => x.Localidad).ThenBy(x => x.Municipio).ThenBy(x => x.EntidadFederativa).ThenBy(x => x.Estado).ThenBy(x => x.Clave).ThenBy(x => x.Cliente).ThenBy(x => x.Colonia).ToList();
                    break;
                case 13:
                    LstClientesAux = LstClientes.OrderBy(x => x.Municipio).ThenBy(x => x.EntidadFederativa).ThenBy(x => x.Estado).ThenBy(x => x.Clave).ThenBy(x => x.Cliente).ThenBy(x => x.Colonia).ThenBy(x => x.Localidad).ToList();
                    break;
                case 14:
                    LstClientesAux = LstClientes.OrderBy(x => x.EntidadFederativa).ThenBy(x => x.Estado).ThenBy(x => x.Clave).ThenBy(x => x.Cliente).ThenBy(x => x.Colonia).ThenBy(x => x.Localidad).ThenBy(x => x.Municipio).ToList();
                    break;

                case 17:
                    LstClientesAux = LstClientes.OrderBy(x => x.Estado).ThenBy(x => x.Clave).ThenBy(x => x.Cliente).ThenBy(x => x.Colonia).ThenBy(x => x.Localidad).ToList();
                    break;

                default:
                    LstClientesAux = LstClientes.OrderBy(x => x.Clave).ThenBy(x => x.Cliente).ThenBy(x => x.Colonia).ThenBy(x => x.Localidad).ThenBy(x => x.Municipio).ThenBy(x => x.EntidadFederativa).ThenBy(x => x.Estado).ToList();
                    break;

            }
        }




    }
}
