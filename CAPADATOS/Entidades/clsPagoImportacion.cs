using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsPagoImportacion
    {
        public string Contrato { get; set; }
        public int NoPago { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string Observacion { get; set; }


    }
}
