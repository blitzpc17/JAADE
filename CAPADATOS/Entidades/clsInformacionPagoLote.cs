using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsInformacionPagoLote
    {
        public int LoteId { get; set; }
        public string IdentificadorLote { get; set; }
        public decimal PrecioLote { get; set; }
        public DateTime FechaContrato { get; set; }
        public decimal PagoInicial { get; set; }
        public int NumeroPagos { get; set; }
        public int ClienteId { get; set; }
        public int PagosRealizados { get; set; }
        public decimal SaldoFavor { get; set; }
        public decimal SaldoContra { get; set; }
        public decimal MontoAtrasado { get; set; }
        public decimal MontoMensualidad { get; set; }
        public decimal MontoExcedePlazo { get; set; }
        public decimal TotalPagar { get; set; }
        public bool ExcedePlazoPago { get; set; }
        public int PagoActual { get; set; }

    }
}
