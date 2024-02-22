using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsInformacionContratoPago
    {
        public int NoUltimoPago { get; set; }
        public decimal SaldoFavor { get; set; }
        public decimal MontoMensualidad { get; set; }
        public int NoPagosRealizados { get; set; }
        public int NoPagoActual { get; set; }
        public int NoPagosContrato { get; set; }
        public int NoPagosGracia { get; set; }
        public int? NoPagoProrrogaActual { get; set; }
        public int? NoUltimoPagoGraciaPago { get; set; }
        public decimal? SaldoProrrogaFavor { get; set; }
        public decimal? MensualidadGracia { get; set; }
        public int NoPagosGraciaRealizados { get; set; } 
    }
}
