using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsPagoData
    {
        public int PagoId { get; set; }
        public string FolioPago { get; set; }
        public int ContratoId { get; set; }
        public string FolioContrato { get; set; }
        public int LoteId { get; set; }
        public string ClaveLote { get; set; }
        public int ZonaId { get; set; }
        public string Zona { get; set; }
        public int DiaPago { get; set; }
        public decimal MontoRecibido { get; set; }
        public DateTime FechaEmision { get; set; }
        public string UsuarioRecibe { get; set; }
        public DateTime? FechaReimpresion { get; set; }
        public string EstadoPago { get; set; }
    }
}
