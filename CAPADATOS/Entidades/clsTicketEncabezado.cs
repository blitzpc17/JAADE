using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsTicketEncabezado
    {
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }   
        public string Cliente { get; set; }
        public decimal PrecioLote { get; set; }
        public int NoPagos { get; set; }
        public string Zona { get; set; }
        public string IdentificadorLote { get; set; }
        public string ObsComportamientoPago { get; set; }
        public string Contrato {  get; set; }

    }
}
