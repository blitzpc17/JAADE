using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsClienteLote
    {
        public int ClienteId { get; set; }
        public int ZonaId { get; set; } 
        public int LoteId { get; set; }
        public string CodigoLote { get; set; }
        public string ZonaNombre { get; set; }  
        public string Cliente { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public int? Manzana { get; set; }
        public decimal PrecioLote { get; set; }   
        public int NoPagos { get; set; }    
        public decimal MontoRestante { get; set; }  
        public int EstadoId { get; set; }
        public string Estado { get; set; }
        public int PagosRegistrados { get; set; }
        public int Id { get; set; }
        

    }
}
