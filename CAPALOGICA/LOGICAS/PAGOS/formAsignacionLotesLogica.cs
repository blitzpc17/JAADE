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
    public class formAsignacionLotesLogica
    {
        private LotesADO contextoLotes;
        private ZonaADO contextoZona;
        private AsignacionClienteLoteADO contextoAsignacion;
        
        public CLIENTE_LOTE ObjClienteLote;
        public clsLotes ObjLoteSeleccionado;
        public clsClienteLote ObjClienteLoteSeleccionado;
        public clsClientes ObjClienteSeleccionado;

        public List<clsClienteLote> LstClienteLotes;
        public List<clsClienteLote> LstClienteLotesAux;
        public List<ZONA> LstZonas;

        public int index;
        public int Column;

        public formAsignacionLotesLogica(int column)
        {
            Column = column;
        }

        public formAsignacionLotesLogica()
        {
            contextoAsignacion = new AsignacionClienteLoteADO();
            contextoZona = new ZonaADO();
            contextoLotes = new LotesADO(); 
        }

        public void InstanciarAsignacionLote()
        {
            ObjClienteLote = new CLIENTE_LOTE();
        }

        public void ListarZonas()
        {
            LstZonas = contextoZona.Listar();
        }

        public void ListarLotes(int zonaId)
        {
            contextoLotes.ListarLotes(zonaId);
        }

        public void Guardar()
        {
            if (ObjClienteLote.Id == 0)
            {
                contextoAsignacion.Insertar(ObjClienteLote);
            }
            contextoAsignacion.Guardar();
        }

        public clsClienteLote ObtenerAsignacion(int id)
        {
            return contextoAsignacion.ObtenerAsignacion(id);
        }

        public List<clsClienteLote> ListarAsignacionesCliente(int clienteId)
        {
            return contextoAsignacion.ListarAsignacionesClientes(clienteId);
        }

        public clsClienteLote ObtenerAsignacionesLotes(int clienteId, int loteId)
        {
            return contextoAsignacion.ObtenerAsignacionesLotes(clienteId, loteId);
        }
        public clsLotes ObtenerLote(int loteId)
        {
            return contextoLotes.ObtenerLoteData(loteId);   
        }

        public bool Filtrar(int column, string termino)
        {
            if (LstClienteLotesAux == null)
            {
                LstClienteLotes = new List<clsClienteLote> ();
                LstClienteLotesAux = LstClienteLotes;
            }


            switch (column)
            {
                case 1:
                    index = LstClienteLotesAux.FindIndex(x => x.CodigoLote.StartsWith(termino));
                    break;

                case 2:
                    index = LstClienteLotesAux.FindIndex(x => x.ZonaNombre.StartsWith(termino));
                    break;

                case 3:
                    index = LstClienteLotesAux.FindIndex(x => x.Cliente.StartsWith(termino));
                    break;


                default:
                    index = LstClienteLotesAux.FindIndex(x => x.CodigoLote.StartsWith(termino));
                    break;

            }

            return (index >= 0);

        }

        public void Ordenar(int column, bool zonaSeleccionada = false)
        {
            switch (column)
            {

                case 1:
                    LstClienteLotesAux = LstClienteLotesAux.OrderBy(x => x.CodigoLote).ThenBy(x => x.ZonaNombre).ThenBy(x => x.Cliente).ThenBy(x => x.Manzana).ToList();
                    break;
                case 2:
                    LstClienteLotesAux = LstClienteLotesAux.OrderBy(x => x.ZonaNombre).ThenBy(x => x.CodigoLote).ThenBy(x => x.Cliente).ThenBy(x => x.Manzana).ToList();
                    break;
                case 3:
                    LstClienteLotesAux = LstClienteLotesAux.OrderBy(x => x.Cliente).ThenBy(x => x.CodigoLote).ThenBy(x => x.ZonaNombre).ThenBy(x => x.Manzana).ToList();
                    break;
                case 4:
                    LstClienteLotesAux = LstClienteLotesAux.OrderBy(x => x.Manzana).ThenBy(x => x.CodigoLote).ThenBy(x => x.ZonaNombre).ThenBy(x => x.Cliente).ToList();
                    break;
                default:
                    LstClienteLotesAux = LstClienteLotesAux.OrderBy(x => x.CodigoLote).ThenBy(x => x.ZonaNombre).ThenBy(x => x.Cliente).ThenBy(x => x.Manzana).ToList();
                    break;

            }
            

        }

        public clsClientes ObtenerClienteData(int clienteId)
        {
            using (var contexto = new ClientesADO())
            {
                return contexto.ObtenerDataCliente(clienteId);
            }
        }
    }
}
