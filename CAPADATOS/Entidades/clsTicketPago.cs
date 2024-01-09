using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsTicketPago
    {
        public clsTicketEncabezado Encabezado { get; set; }
        public List<clsTicketPartida> Partidas { get; set; }
    }
}
