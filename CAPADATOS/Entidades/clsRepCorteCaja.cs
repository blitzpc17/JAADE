using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsRepCorteCaja
    {
        public int PagoId { get; set; }
        public string FolioPago { get; set; }
        public DateTime FechaEmisionPago { get; set; }      
        public string FolioContrato { get; set; }
        public string IdentificadorLote { get; set; }
        public decimal Monto { get; set; }        
        public string NombreRecibio { get; set; }
        public string Observacion { get; set; }


    }
}
