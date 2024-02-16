using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsContratoCliente
    {
        public int ContratoId { get; set; }
        public string Folio { get; set; }
        public DateTime FechaEmision { get; set; }
        public int ClienteId { get; set; }
        public string ClaveCliente { get; set; }    
        public string ClienteNombre { get; set; }      
        public int EstadoId { get; set; }
        public string Estado { get; set; }
        public string ClaveLote { get; set; }
        public string ZonaLote { get; set; }
        public int LoteId { get; set; }
        public int ZonaLoteId { get; set; }
        public int SocioId { get; set; }
        public decimal PrecioLote { get; set; }
        public int NoPagos { get; set; }
        public int DiaPago { get; set; }       
        public string UsuarioRealizo { get; set; }
        public DateTime? FechaReImpresion { get; set; }
        public int PagosRealizados { get; set; }
        public decimal PagoInicial { get; set; }
        public int NoPagosGracia { get; set; }
        public decimal? MontoGracia { get; set; }
        public string Observacion { get; set; }
       

    }
}
