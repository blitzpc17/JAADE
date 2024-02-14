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
        public string ClaveCliente { get; set; }    
        public string ClienteNombre { get; set; }
        public int ClienteId { get; set; }
        public string ClaveLote { get; set; }
        public string ZonaLote { get; set; }
        public int LoteId { get; set; }
        public int ZonaLoteId { get; set; }
        public List<SOCIOS> ListaSocios { get; set; }
        public decimal PrecioLote { get; set; }
        public int NoPagos { get; set; }
        public int DiaPago { get; set; }
        public DateTime FechaEmision { get; set; }
        public string UsuarioRealizo { get; set; }
        public DateTime? FechaReImpresion { get; set; }

    }
}
