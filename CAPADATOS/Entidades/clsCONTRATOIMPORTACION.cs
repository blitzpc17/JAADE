using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsCONTRATOIMPORTACION
    {
        public DateTime FechaArrendamiento { get; set; }
        public string ClaveCliente { get; set; }
        public string Socio { get; set; }
        public string Zona { get; set; }
        public string IdentificadorLote { get; set; }
        public int NoPagos { get; set; }
        public decimal PrecioInicial { get; set; }
        public int DiaPago { get; set; }
        public decimal PagoInicial { get; set; }
        public int NoPagosGracia { get; set; }
        public string Observacion { get; set; }

    }
}
