using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsCalculoMontoPagado
    {
        public decimal SaldoFavor { get; set; }
        public decimal SaldoContra { get; set; }
        public int NoPagosDados { get; set; }
        public int NoPagoActual { get; set; }

    }
}
