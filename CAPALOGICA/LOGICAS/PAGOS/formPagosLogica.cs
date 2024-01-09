using CAPADATOS;
using CAPADATOS.ADO.PAGOS;
using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPALOGICA.LOGICAS.PAGOS
{
    public class formPagosLogica
    {
        private PagosADO contexto;
        public List<clsPago> LstPagos;
        public List<clsPago> LstPagosAux;

        public clsTicketPago ObjTicketPago;
        public clsClientes ObjCliente;
        public clsLotes ObjLotes;
        public clsPago ObjPagoDato;
        public PAGO ObjPago;
        public clsUsuario ObjUsuarioRecibe;   

        public int Index=-1;

        public formPagosLogica()
        {
            contexto = new PagosADO();
        }

        public void InstanciarPago()
        {
            ObjPago = new PAGO();
        }

        public void Guardar()
        {
            if (ObjPago.Id == 0)
            {
                contexto.Insertar(ObjPago);             
            }
            contexto.Guardar();
        }

        public clsPago ObtenerPago(int id)
        {
            return contexto.ObtenerDataPago(id);
        }
        public List<clsPago> ListarPagos(int? clienteId)
        {
            return contexto.ListarPagos(clienteId);
        }
     

        public bool Filtrar(int column, string termino)
        {
            if (LstPagosAux == null) LstPagosAux = LstPagos;

            switch (column)
            {
                case 1:
                    Index = LstPagosAux.FindIndex(x => x.NumeroReferencia.StartsWith(termino));
                    break;
             


                default:
                    Index = -1;
                    break;

            }

            return (Index >= 0);

        }

        public void Ordenar(int column)
        {
            switch (column)
            {

                case 1:
                    LstPagosAux = LstPagos.OrderBy(x => x.NumeroReferencia).ThenBy(x => x.FechaEmision).ThenBy(x => x.Cliente).ThenBy(x => x.IdentificadorLote).ThenBy(x => x.Usuario).ToList();
                    break;
               

                default:
                    LstPagosAux = LstPagos.OrderBy(x => x.NumeroReferencia).ThenBy(x => x.FechaEmision).ThenBy(x => x.Cliente).ThenBy(x => x.IdentificadorLote).ThenBy(x => x.Usuario).ToList(); 
                    break;

            }
        }
    }
}
