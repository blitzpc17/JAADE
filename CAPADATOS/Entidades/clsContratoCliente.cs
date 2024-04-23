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
        public string NoReferencia { get; set; }
        public DateTime FechaEmision { get; set; }
        public int ClienteId { get; set; }
        public string ClaveCliente { get; set; }   
        public string NombreCliente { get; set; }
        public int EstadoId { get; set; }
        public string NombreEstado { get; set; }
        public int ZonaId { get; set; }
        public string ZonaNombre { get; set; }
        public string LotesRelacionados { get; set; }        
        public int? SocioId { get; set; }
        public string SocioNombre { get; set; }
        public decimal PrecioLote { get; set; }
        public int NoPagos { get; set; }    
        public int DiaPago { get; set; }
        public decimal PagoInicial { get; set; }
        public int NoPagosGracia { get; set; }
        public decimal? MontoGracia { get; set; }      
        public int? ContratoOrigenId { get; set; }
        public string FolioContratoOrigen { get; set; }
        public string Observacion { get; set; }       
        public int UsuarioOperacionId { get; set; }
        public string UsuarioOperacionNombre { get; set; }
        public DateTime? FechaReimpresion { get; set; }
        public int DomicilioClienteId { get; set; }
        public string DomicilioCliente { get; set; }
        public decimal MideNorte { get; set; }
        public decimal MideSur { get; set; }
        public decimal MideEste { get; set; }
        public decimal MideOeste { get; set; }
        public string ColindaNorte { get; set; }
        public string ColindaSur { get; set; }
        public string ColindaEste { get; set; }
        public string ColindaOeste { get; set; }
        public decimal MontoDado { get; set; }
        public int NoPagosDados { get; set; }
        public DateTime FechaUltimoPago { get; set; }
        public decimal? MontoExtendidoDado {  get; set; }
        public int NoPagosExtendidosDados { get; set; }

    }
}
