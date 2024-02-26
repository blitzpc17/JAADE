using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsArrendamientoLoteData
    {
        public string NoReferencia { get; set; }
        public DateTime FechaEmision { get; set; }  
        public string IdentificadorLote { get; set; }
        public string ClaveZona { get; set; }
        public string DomicilioZona { get; set; }
        public string CEste { get; set; }
        public string CNorte { get; set; }
        public string COeste { get; set; }
        public string CSur { get; set; }
        public decimal MEste { get; set; }
        public decimal MNorte { get; set; }
        public decimal MOeste { get; set; }
        public decimal MSur { get; set; }
        public decimal PrecioLote { get; set; }
        public string NombreCliente { get; set; }
        public int DiaPago { get; set; }
        public decimal PagoInicial { get; set; }
        public int NoPagos { get; set; }
        public string DomicilioCliente { get; set; }
        public int? Manzana { get; set; }
        public int NoPagosGracia { get; set; }
    }
}
