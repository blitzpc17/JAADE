using CAPADATOS.ADO.LOTES;
using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPALOGICA.LOGICAS.BUSQUEDA
{
    public class busLotesZonaLogica
    {
        private LotesADO contexto;
        public List<clsLotes> LstLote;
        public List<clsLotes> LstLoteAux;

        public int index = -1;
        public int indexAux = -1;
        public int Column = 0;
        public int ZonaId = -1, ClienteId = -1;

        public busLotesZonaLogica()
        {
            contexto = new LotesADO();
        }

        public void ListarRegistros(bool busquedaCliente= false)
        {
            if (busquedaCliente)
            {
                LstLote = contexto.ListarLotes(ClienteId, true);
            }
            else
            {
                LstLote = contexto.ListarLotes(ZonaId);
            }
            
        }

        public clsLotes ObtenerRegistro(int id)
        {
            return contexto.ObtenerLoteData(id);
        }

        public bool Filtrar(int column, string termino)
        {
            if (LstLoteAux == null) LstLoteAux = LstLote;

            switch (column)
            {
                case 1:
                    index = LstLoteAux.FindIndex(x => x.Identificador.StartsWith(termino));
                    break;
                case 14:
                    index = LstLoteAux.FindIndex(x => x.Manzana.ToString().StartsWith(termino));
                    break;
                case 16:
                    index = LstLoteAux.FindIndex(x => x.Estado.StartsWith(termino));
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
                    LstLoteAux = LstLote.OrderBy(x => x.Identificador).ThenBy(x => x.Manzana).ThenBy(x => x.Precio).ThenBy(x => x.Estado).ToList();
                    break;

                case 16:
                    LstLoteAux = LstLote.OrderBy(x => x.Estado).ThenBy(x => x.Identificador).ThenBy(x => x.Manzana).ThenBy(x => x.Precio).ToList();
                    break;

                default:
                    LstLoteAux = LstLote.OrderBy(x => x.Identificador).ThenBy(x => x.Manzana).ThenBy(x => x.Precio).ThenBy(x => x.Estado).ToList();
                    break;

            }
        }





    }
}
