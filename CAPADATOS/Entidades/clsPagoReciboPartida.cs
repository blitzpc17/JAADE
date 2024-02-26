using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsPagoReciboPartida
    {
        public int ContratoId { get; set; } 
        public int PagoId { get; set; }
        public string Folio { get; set; }
        public int NoPago { get; set; } 
        public decimal Monto { get; set; }        
        public DateTime FechaEmision { get; set; }
        public string Observacion { get; set; }
    }
}
