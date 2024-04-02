using CAPADATOS.ADO.PAGOS;
using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPALOGICA.LOGICAS.BUSQUEDA
{
    public class busContratoLogica
    {
        private ContratoLoteADO contexto;
        public List<clsContratoCliente> LstContratos;
        public List<clsContratoCliente> LstContratosAux;

        public int index = -1;
        public int indexAux = -1;
        public int Column = 0;

        public busContratoLogica()
        {
            contexto = new ContratoLoteADO();
        }        

        public void ListarRegistros()
        {
            LstContratos = contexto.ListarContratosCliente();
        }

        public clsContratoCliente ObtenerRegistro(string folio)
        {
            return contexto.ObtenerContratoClienteFolio(folio);
        }

        public clsContratoCliente ObtenerRegistroEnListaAux(int contratoId)
        {
            return LstContratosAux.FirstOrDefault(x => x.ContratoId == contratoId);
        }

        public bool Filtrar(int column, string termino)
        {
            if (LstContratosAux == null) LstContratosAux = LstContratos;

            switch (column)
            {
                case 1:
                    index = LstContratosAux.FindIndex(x => x.NoReferencia.StartsWith(termino));
                    break;

                case 2:
                    index = LstContratosAux.FindIndex(x => x.FechaEmision.ToString("dd/MM/yyyy").StartsWith(termino));
                    break;

                case 4:
                    index = LstContratosAux.FindIndex(x => x.ClaveCliente.StartsWith(termino));
                    break;

                case 5:
                    index = LstContratosAux.FindIndex(x => x.NombreCliente.StartsWith(termino));
                    break;

                case 7:
                    index = LstContratosAux.FindIndex(x => x.NombreEstado.StartsWith(termino));
                    break;

                case 8:
                    index = LstContratosAux.FindIndex(x => x.ZonaNombre.StartsWith(termino));
                    break;

                case 9:
                    index = LstContratosAux.FindIndex(x => x.LotesRelacionados.StartsWith(termino));
                    break;

                case 11:
                    index = LstContratosAux.FindIndex(x => x.SocioNombre.StartsWith(termino));
                    break;               

                case 12:
                    index = LstContratosAux.FindIndex(x => x.PrecioLote.ToString("N2").StartsWith(termino));
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
                    LstContratosAux = LstContratos.OrderBy(x => x.NoReferencia).
                        ThenBy(x => x.FechaEmision).ThenBy(x => x.ClaveCliente).
                        ThenBy(x=>x.NombreCliente).ThenBy(x=>x.NombreEstado)
                        .ThenBy(x=>x.ZonaNombre).ThenBy(x=>x.LotesRelacionados)
                        .ThenBy(x=>x.SocioNombre).ThenBy(x=>x.PrecioLote)
                        .ToList();
                    break;

                case 2:
                    LstContratosAux = LstContratos.OrderBy(x => x.NoReferencia).
                        ThenBy(x => x.FechaEmision).ThenBy(x => x.ClaveCliente).
                        ThenBy(x => x.NombreCliente).ThenBy(x => x.NombreEstado)
                        .ThenBy(x => x.ZonaNombre).ThenBy(x => x.LotesRelacionados)
                        .ThenBy(x => x.SocioNombre).ThenBy(x => x.PrecioLote)
                        .ToList();
                    break;

                case 4:
                    LstContratosAux = LstContratos.OrderBy(x => x.NoReferencia).
                        ThenBy(x => x.FechaEmision).ThenBy(x => x.ClaveCliente).
                        ThenBy(x => x.NombreCliente).ThenBy(x => x.NombreEstado)
                        .ThenBy(x => x.ZonaNombre).ThenBy(x => x.LotesRelacionados)
                        .ThenBy(x => x.SocioNombre).ThenBy(x => x.PrecioLote)
                        .ToList();
                    break;


                case 5:
                    LstContratosAux = LstContratos.OrderBy(x => x.NoReferencia).
                        ThenBy(x => x.FechaEmision).ThenBy(x => x.ClaveCliente).
                        ThenBy(x => x.NombreCliente).ThenBy(x => x.NombreEstado)
                        .ThenBy(x => x.ZonaNombre).ThenBy(x => x.LotesRelacionados)
                        .ThenBy(x => x.SocioNombre).ThenBy(x => x.PrecioLote)
                        .ToList();
                    break;

                case 7:
                    LstContratosAux = LstContratos.OrderBy(x => x.NoReferencia).
                        ThenBy(x => x.FechaEmision).ThenBy(x => x.ClaveCliente).
                        ThenBy(x => x.NombreCliente).ThenBy(x => x.NombreEstado)
                        .ThenBy(x => x.ZonaNombre).ThenBy(x => x.LotesRelacionados)
                        .ThenBy(x => x.SocioNombre).ThenBy(x => x.PrecioLote)
                        .ToList();
                    break;

                case 8:
                    LstContratosAux = LstContratos.OrderBy(x => x.NoReferencia).
                        ThenBy(x => x.FechaEmision).ThenBy(x => x.ClaveCliente).
                        ThenBy(x => x.NombreCliente).ThenBy(x => x.NombreEstado)
                        .ThenBy(x => x.ZonaNombre).ThenBy(x => x.LotesRelacionados)
                        .ThenBy(x => x.SocioNombre).ThenBy(x => x.PrecioLote)
                        .ToList();
                    break;

                case 9:
                    LstContratosAux = LstContratos.OrderBy(x => x.NoReferencia).
                        ThenBy(x => x.FechaEmision).ThenBy(x => x.ClaveCliente).
                        ThenBy(x => x.NombreCliente).ThenBy(x => x.NombreEstado)
                        .ThenBy(x => x.ZonaNombre).ThenBy(x => x.LotesRelacionados)
                        .ThenBy(x => x.SocioNombre).ThenBy(x => x.PrecioLote)
                        .ToList();
                    break;

                case 11:
                    LstContratosAux = LstContratos.OrderBy(x => x.NoReferencia).
                        ThenBy(x => x.FechaEmision).ThenBy(x => x.ClaveCliente).
                        ThenBy(x => x.NombreCliente).ThenBy(x => x.NombreEstado)
                        .ThenBy(x => x.ZonaNombre).ThenBy(x => x.LotesRelacionados)
                        .ThenBy(x => x.SocioNombre).ThenBy(x => x.PrecioLote)
                        .ToList();
                    break;

                case 12:
                    LstContratosAux = LstContratos.OrderBy(x => x.NoReferencia).
                        ThenBy(x => x.FechaEmision).ThenBy(x => x.ClaveCliente).
                        ThenBy(x => x.NombreCliente).ThenBy(x => x.NombreEstado)
                        .ThenBy(x => x.ZonaNombre).ThenBy(x => x.LotesRelacionados)
                        .ThenBy(x => x.SocioNombre).ThenBy(x => x.PrecioLote)
                        .ToList();
                    break;




                default:
                    LstContratosAux = LstContratos.OrderBy(x => x.NoReferencia).
                      ThenBy(x => x.FechaEmision).ThenBy(x => x.ClaveCliente).
                      ThenBy(x => x.NombreCliente).ThenBy(x => x.NombreEstado)
                      .ThenBy(x => x.ZonaNombre).ThenBy(x => x.LotesRelacionados)
                      .ThenBy(x => x.SocioNombre).ThenBy(x => x.PrecioLote)
                      .ToList();
                    break;

            }
        }






    }
}
