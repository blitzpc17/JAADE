using CAPADATOS;
using CAPADATOS.ADO.LOTES;
using CAPADATOS.ADO.SISTEMA;
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
        private EstadoADO contextoEstado;
        public LOTE ObjLote;
        public clsLotes ObjLoteData;
        public List<clsLotes> LstLote;
        public List<clsLotes> LstLoteAux;        
        public List<ZONA> LstZona;
        public List<ESTADO> LstEstados;
        public ZONA ObjZona;

        public int index = -1;
        public int indexAux = -1;
        public int Column = 0;

        public formLotesLogica()
        {
            contextoLotes = new LotesADO();
            contextoZonas = new ZonaADO();
            contextoEstado = new EstadoADO();
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

        public void ListarEstadosProceso(string nombreProceso)
        {
            LstEstados = contextoEstado.Listar().Where(x => x.Proceso == nombreProceso).ToList();
        }
        public ESTADO ObtenerEstadoLote(string nombreEstado)
        {
            return LstEstados.FirstOrDefault(x => x.Nombre == nombreEstado);            
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
                    LstLoteAux = LstLote.OrderBy(x => x.Identificador).ThenBy(x => x.Manzana).ThenBy(x=>x.Precio).ThenBy(x => x.Estado).ToList();
                    break;

                case 16:
                    LstLoteAux = LstLote.OrderBy(x => x.Estado).ThenBy(x=>x.Identificador).ThenBy(x=>x.Manzana).ThenBy(x => x.Precio).ToList();
                    break;

                default:
                    LstLoteAux = LstLote.OrderBy(x => x.Identificador).ThenBy(x => x.Manzana).ThenBy(x => x.Precio).ThenBy(x => x.Estado).ToList();
                    break;

            }
        }

        public int ObtenerUltimoLote(int ZonaId)
        {
            return contextoLotes.ObtenerUltimoLote(ZonaId);
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
