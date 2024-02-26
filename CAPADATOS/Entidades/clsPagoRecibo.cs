using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsPagoRecibo
    {
        public clsPagoReciboEncabezado Encabezado { get; set; }
        public List<clsPagoReciboPartida> LstPartidas { get; set; }

    }
}
