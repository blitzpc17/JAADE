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
    public class formAsignacionLotesLogica
    {
        private LotesADO contextoLotes;
        private ZonaADO contextoZona;
        private AsignacionClienteLoteADO contextoAsignacion;
        private PagosADO contextoPagos;

        public CLIENTE_LOTE ObjClienteLote;
        public clsLotes ObjLoteSeleccionado;
        public clsClienteLote ObjClienteLoteSeleccionado;
        public clsClientes ObjClienteSeleccionado;

        public List<clsClienteLote> LstClienteLotes;
        public List<clsClienteLote> LstClienteLotesAux;
        public List<ZONA> LstZonas;
        public List<PAGO> LstPagosAsociadosClienteLote;
        public PAGO ObjPago;

        private LOTE ObjLote;
        private LOTE ObjLoteAnt;
        public List<ESTADO> LstEstadosLote;

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
            contextoPagos = new PagosADO();
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
        public clsClientes ObtenerClienteData(int clienteId)
        {
            using (var contexto = new ClientesADO())
            {
                return contexto.ObtenerDataCliente(clienteId);
            }
        }
        public void ListarEstadosProcesoLotes(string nombreProceso)
        {
            using (var contexto = new EstadoADO())
            {
                LstEstadosLote = contexto.Listar().Where(x => x.Proceso == nombreProceso).ToList();
            }
        }
        public bool ActualizarEstadoLote(int idLote, string estado)
        {
            try
            {
                ObjLote = contextoLotes.Obtener(idLote);
                ObjLote.ESTADOId = LstEstadosLote.First(x => x.Nombre == estado).Id;
                contextoLotes.Guardar();
                return true;

            } catch (Exception ex)
            {
                return false;
            }
        }

        public bool ReasignarPagosLote(int idLoteAnterior, int clienteId, int idLoteNuevo)
        {
            try
            {
                LstPagosAsociadosClienteLote = contextoPagos.ListarPagosAsociadosLote(clienteId, idLoteAnterior);
                LstPagosAsociadosClienteLote.ForEach(x => x.LOTEId = idLoteNuevo);
                contextoPagos.Guardar();
                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }

        public void GuardarPagoInicial()
        {
            contextoPagos.Insertar(ObjPago);
            contextoPagos.Guardar();
        }
        public void InstanciarObjetoPago()
        {
            ObjPago = new PAGO();
        }

        public PAGO ObtenerPagoInicial(int idCliente, int loteId)
        {
            LstPagosAsociadosClienteLote = contextoPagos.ListarPagosAsociadosLote(idCliente, loteId);
            return (LstPagosAsociadosClienteLote== null || LstPagosAsociadosClienteLote.Count==0)? null: LstPagosAsociadosClienteLote.First(x=>x.NumeroPago=="1");
        }

        public CLIENTE_LOTE ObtenerAsignacionClienteLote(int id)
        {
            return contextoAsignacion.Obtener(id);
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

      
    }
}
