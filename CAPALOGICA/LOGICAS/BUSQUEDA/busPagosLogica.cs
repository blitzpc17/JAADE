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
        private PagoADO contexto;
        public List<clsPago> LstPagos;
        public List<clsPago> LstPagosAux;

        public int index = -1;
        public int indexAux = -1;
        public int Column = 0;
        public int ClienteId = -1;


        public busPagosLogica()
        {
            contexto = new PagoADO();
        }

        public void ListarRegistros(int? clienteId)
        {
           // LstPagos = contexto.listar(clienteId );
        }

        public clsPago ObtenerRegistro(int id)
        {
            return null;
            //return contexto.ObtenerDataPago(id);
        }

        public bool Filtrar(int column, string termino)
        {
            if (LstPagosAux == null) LstPagosAux = LstPagos;

            switch (column)
            {
                case 1:
                    index = LstPagosAux.FindIndex(x => x.NumeroReferencia.StartsWith(termino));
                    break;

                case 4:
                    index = LstPagosAux.FindIndex(x => x.Cliente.StartsWith(termino));
                    break;

                case 6:
                    index = LstPagosAux.FindIndex(x => x.IdentificadorLote.StartsWith(termino));
                    break;

                case 8:
                    index = LstPagosAux.FindIndex(x => x.Zona.StartsWith(termino));
                    break;

                case 9:
                    index = LstPagosAux.FindIndex(x => x.Manzana.ToString().StartsWith(termino));
                    break;

                case 11:
                    index = LstPagosAux.FindIndex(x => x.Usuario.StartsWith(termino));
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
                    LstPagosAux = LstPagos.OrderBy(x => x.NumeroReferencia)
                        .ThenBy(x=>x.Cliente).ThenBy(x => x.IdentificadorLote).ThenBy(x=>x.Zona).ThenBy(x=>x.Manzana)
                        .ThenBy(x=>x.Usuario).ToList();
                    break;
                case 4:
                    LstPagosAux = LstPagos.OrderBy(x => x.Cliente)
                       .ThenBy(x => x.NumeroReferencia).ThenBy(x => x.IdentificadorLote).ThenBy(x => x.Zona).ThenBy(x => x.Manzana)
                       .ThenBy(x => x.Usuario).ToList();
                    break;
                case 6:
                    LstPagosAux = LstPagos.OrderBy(x => x.IdentificadorLote).ThenBy(x => x.Cliente)
                                    .ThenBy(x => x.NumeroReferencia).ThenBy(x => x.Zona).ThenBy(x => x.Manzana)
                                    .ThenBy(x => x.Usuario).ToList();
                    break;
                case 8:
                    LstPagosAux = LstPagos.OrderBy(x => x.Zona).ThenBy(x => x.IdentificadorLote).ThenBy(x => x.Cliente)
                                    .ThenBy(x => x.NumeroReferencia).ThenBy(x => x.Manzana)
                                    .ThenBy(x => x.Usuario).ToList();
                    break;
                case 9:
                    LstPagosAux = LstPagos.OrderBy(x => x.Manzana).ThenBy(x => x.Zona).ThenBy(x => x.IdentificadorLote).ThenBy(x => x.Cliente)
                                    .ThenBy(x => x.NumeroReferencia)
                                    .ThenBy(x => x.Usuario).ToList();
                    break;
                case 11:
                    LstPagosAux = LstPagos.OrderBy(x => x.Usuario).ThenBy(x => x.Manzana).ThenBy(x => x.Zona).ThenBy(x => x.IdentificadorLote).ThenBy(x => x.Cliente)
                                    .ThenBy(x => x.NumeroReferencia)
                                    .ToList();
                    break;

                default:
                    LstPagosAux = LstPagos.OrderBy(x => x.NumeroReferencia)
                .ThenBy(x => x.Cliente).ThenBy(x => x.IdentificadorLote).ThenBy(x => x.Zona).ThenBy(x => x.Manzana)
                .ThenBy(x => x.Usuario).ToList();
                    break;

            }
        }


    }
}
