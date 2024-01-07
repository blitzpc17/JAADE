using CAPADATOS;
using CAPADATOS.ADO.LOTES;
using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPALOGICA.LOGICAS.LOTES
{
    public class formLotesLogica
    {

        private LotesADO contextoLotes;
        private ZonaADO contextoZonas;
        public LOTE ObjLote;
        public clsLotes ObjLoteData;
        public List<clsLotes> LstLote;
        public List<clsLotes> LstLoteAux;        
        public List<ZONA> LstZona;
        public ZONA ObjZona;

        public int index = -1;
        public int indexAux = -1;
        public int Column = 0;

        public formLotesLogica()
        {
            contextoLotes = new LotesADO();
            contextoZonas = new ZonaADO();
        }

        public void InstanciarLote()
        {
            ObjLote = new LOTE();
        }

        public void Guardar()
        {
            if (ObjLote.Id == 0)
            {
                contextoLotes.Insertar(ObjLote);
            }

            contextoLotes.Guardar();
        }

        public void ListarLotesZonas(int zonaId)
        {
            LstLote = contextoLotes.ListarLotes(zonaId);
            LstLoteAux = LstLote;
        }

        public LOTE Obtener(int id)
        {
            return contextoLotes.Obtener(id);
        }

        public void Eliminar(LOTE entidad)
        {
            contextoLotes.Eliminar(entidad);
        }


        public bool Filtrar(int column, string termino)
        {
            if (LstLoteAux == null) LstLoteAux = LstLote;

            switch (column)
            {
                case 1:
                    index = LstLoteAux.FindIndex(x => x.Identificador.StartsWith(termino));
                    break;               

                default:
                    index = -1;
                    break;

            }

            return (index >= 0);

        }

        public int ObtenerUltimoLote(int ZonaId)
        {
            return contextoLotes.ObtenerUltimoLote(ZonaId);
        }

        public void Ordenar(int column)
        {
            switch (column)
            {

                case 1:
                    LstLoteAux = LstLote.OrderBy(x => x.Identificador).ThenBy(x=>x.Precio).ToList();
                    break;

                default:
                    LstLoteAux = LstLote.OrderBy(x => x.Identificador).ThenBy(x => x.Precio).ToList();
                    break;

            }
        }

        public void ListarCatalogos()
        {
            LstZona = contextoZonas.Listar();
        }

        public void ObtenerZona(int idZona)
        {
            ObjZona = contextoZonas.Obtener(idZona);
        }

        public void ObtenerLote(int LoteId)
        {
            ObjLoteData = contextoLotes.ObtenerLoteData(LoteId);
        }
    }
}
