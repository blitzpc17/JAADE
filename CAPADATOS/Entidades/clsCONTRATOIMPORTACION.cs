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
        public int NoPagos { get; set; }
        public decimal PrecioInicial { get; set; }
        public int DiaPago { get; set; }
        public int NoPagosGracia { get; set; }
        public string Observacion { get; set; }
        public string Zona { get; set; }
        public string IdentificadorLote { get; set; }
        public string ColindaNorte {  get; set; }
        public string ColindaSur { get; set; }
        public string ColindaEste { get; set; }
        public string ColindaOeste { get; set; }
        public decimal MideNorte {  get; set; }
        public decimal MideSur { get; set; }
        public decimal MideEste { get; set; }
        public decimal MideOeste { get; set; }
        public string Direccion { get; set; }
        public string Estado { get; set; }
        public decimal PagoInical { get; set; }
    }
}
