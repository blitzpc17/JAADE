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
        public List<clsBusquedaPago> LstPagos;
        public List<clsBusquedaPago> LstPagosAux;

        public int index = -1;
        public int indexAux = -1;
        public int Column = 0;
        public int ClienteId = -1;


        public busPagosLogica()
        {
            contexto = new PagoADO();
        }

        public void ListarRegistros(string folioContrato)
        {
            LstPagos = contexto.ListarPagosContrato(folioContrato);
        }

        public clsBusquedaPago ObtenerRegistro(string folio)
        {
            return LstPagosAux.FirstOrDefault(x=>x.Folio == folio );
        }

        public bool Filtrar(int column, string termino)
        {
            if (LstPagosAux == null) LstPagosAux = LstPagos;

            switch (column)
            {
                case 1:
                    index = LstPagosAux.FindIndex(x => x.Folio.StartsWith(termino));
                    break;

                case 4:
                    index = LstPagosAux.FindIndex(x => x.Contrato.StartsWith(termino));
                    break;

                case 5:
                    index = LstPagosAux.FindIndex(x => x.Cliente.StartsWith(termino));
                    break;

                case 6:
                    index = LstPagosAux.FindIndex(x => x.Zona.StartsWith(termino));
                    break;

                case 7:
                    index = LstPagosAux.FindIndex(x => x.Identificador.ToString().StartsWith(termino));
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
                    LstPagosAux = LstPagos.OrderBy(x => x.Folio)
                        .ThenBy(x=>x.Contrato).ThenBy(x => x.Cliente)
                        .ThenBy(x=>x.Zona).ThenBy(x=>x.Identificador).ToList();
                    break;
                case 4:
                    LstPagosAux = LstPagos.OrderBy(x => x.Contrato)
                        .ThenBy(x => x.Cliente).ThenBy(x => x.Zona)
                        .ThenBy(x => x.Identificador).ThenBy(x => x.Folio).ToList();
                    break;
                case 6:
                    LstPagosAux = LstPagos.OrderBy(x => x.Cliente)
                      .ThenBy(x => x.Zona).ThenBy(x => x.Identificador)
                      .ThenBy(x => x.Folio).ThenBy(x => x.Contrato).ToList();
                    break;
                case 8:
                    LstPagosAux = LstPagos.OrderBy(x => x.Zona)
                        .ThenBy(x => x.Identificador).ThenBy(x => x.Folio)
                        .ThenBy(x => x.Contrato).ThenBy(x => x.Cliente)
                        .ToList();
                    break;
                case 9:
                    LstPagosAux = LstPagos.OrderBy(x => x.Identificador)
                        .ThenBy(x => x.Folio)
                        .ThenBy(x => x.Contrato).ThenBy(x => x.Cliente)
                        .ThenBy(x => x.Zona).ToList();
                    break;
               

                default:
                    LstPagosAux = LstPagos.OrderBy(x => x.Folio)
                      .ThenBy(x => x.Contrato).ThenBy(x => x.Cliente)
                      .ThenBy(x => x.Zona).ThenBy(x => x.Identificador).ToList();
                    break;

            }
        }


    }
}
