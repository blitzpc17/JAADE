using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsPagoReciboEncabezado
    {
        public int ContratoId { get; set; }
        public string FolioContrato { get; set; }
        public int ClienteId { get;set; }
        public string ClaveCliente { get; set; }
        public string NombreCliente { get; set; }
        public string Socio { get; set; }
        public int ZonaId { get; set; }
        public string ZonaNombre { get; set; }
        public int LoteId { get; set; }
        public string LoteIdentificador { get; set; }
        public decimal PrecioLote { get; set; }
        public int NoPagos { get; set; }
        public DateTime FechaArrendamiento { get; set; }
        public string Observacion { get; set; }
    }
}
