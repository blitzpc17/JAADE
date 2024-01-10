using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsPago
    {
        public int PagoId { get; set; }
        public string NumeroReferencia { get; set; }
        public DateTime FechaEmision { get; set; }
        public int ClienteId { get; set; }
        public string Cliente { get; set; }
        public int LoteId { get; set; }
        public string IdentificadorLote { get; set; }
        public int ZonaId { get; set; }
        public string Zona { get; set; }
        public int? Manzana { get; set; }
        public decimal Monto { get; set; }
        public int UsuarioRecibeId { get; set; }
        public string Usuario { get; set; } 
        public int NoPagos { get; set; }
        public decimal PrecioLote { get; set; }
        public int PagosRealizados { get; set; }
        public decimal SaldoAcumulado { get; set; }


    }
}
