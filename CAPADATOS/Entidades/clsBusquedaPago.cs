using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsBusquedaPago
    {
        public int PagoId { get; set; }
        public string Folio { get; set; }
        public DateTime FechaEmision { get; set; }
        public int ContratoId { get; set; }
        public string Contrato { get; set; }
        public string Cliente { get; set; }
        public string Zona { get; set; }
        public string Identificador { get; set; }
        public decimal Monto { get; set; }
    }

}
