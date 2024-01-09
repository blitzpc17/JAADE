using CAPADATOS.ADO.PAGOS;
using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPALOGICA.LOGICAS.BUSQUEDA
{
    public class busPagosLogica
    {
        private PagosADO contexto;
        public List<clsPago> LstPagos;
        public List<clsPago> LstPagosAux;

        public int index = -1;
        public int indexAux = -1;
        public int Column = 0;
        public int ClienteId = -1;


        public busPagosLogica()
        {
            contexto = new PagosADO();
        }

        public void ListarRegistros(int? clienteId)
        {
            LstPagos = contexto.ListarPagos(clienteId );
        }

        public clsPago ObtenerRegistro(int id)
        {
            return contexto.ObtenerDataPago(id);
        }

        public bool Filtrar(int column, string termino)
        {
            if (LstPagosAux == null) LstPagosAux = LstPagos;

            switch (column)
            {
                case 1:
                    index = -1;//LstPagosAux.FindIndex(x => x.Identificador.StartsWith(termino));
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
                  //  LstPagosAux = LstPagos.OrderBy(x => x.Identificador).ThenBy(x => x.Precio).ToList();
                    break;

                default:
                    //LstPagosAux = LstPagos.OrderBy(x => x.Identificador).ThenBy(x => x.Precio).ToList();
                    break;

            }
        }


    }
}
