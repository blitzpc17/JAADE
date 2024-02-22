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
        public int LoteId { get; set; }
        public string IdentificadorLote { get; set; }
        public int ZonaId { get; set; }
        public string NombreZona { get; set; }
        public int EstadoId { get; set; }
        public string NombreEstado { get; set; }
        public int? SocioId { get; set; }
        public string SocioNombre { get; set; }
        public decimal PrecioLote { get; set; }
        public int NoPagos { get; set; }    
        public int DiaPago { get; set; }
        public decimal PagoInicial { get; set; }
        public int NoPagosGracia { get; set; }
        public decimal? MontoGracia { get; set; }
        public decimal? MensualidadGracia { get; set; }       
        public int? ContratoReubicadoId { get; set; }
        public string ContratoReubicado { get; set; }
        public string Observacion { get; set; }       
        public int UsuarioOperacionId { get; set; }
        public string UsuarioOperacionNombre { get; set; }
        public DateTime? FechaReimpresion { get; set; }


    }
}
