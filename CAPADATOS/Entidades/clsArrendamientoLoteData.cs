using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsArrendamientoLoteData
    {
        public string NumeroReferencia { get; set; }
        public DateTime FechaEmision { get; set; }   //este es provisional
        public string LotesRelacionados { get; set; }
        public string NoLotesRelacionados { get; set; }
        public string NombreCliente { get; set; }
        public int DiaPago { get; set; }
        public decimal PagoInicial { get; set; }
        public int NoPagos { get; set; }
        public int NoPagosExtendido { get; set; }
        public string ColindaNorte { get; set; }
        public string ColindaSur { get; set; }
        public string ColindaOeste { get; set; }
        public string ColindaEste { get; set; }
        public decimal MideNorte { get; set; }
        public decimal MideSur { get; set; }
        public decimal MideEste { get; set; }
        public decimal MideOeste { get; set; }
        public string DomicilioCliente { get; set; }
        public string Hora { get; set; }
        public string Minuto { get; set; }
        public string Dia { get; set; }
        public string Mes { get; set; }
        public string Anio { get; set; }
        public string CalleJaade { get; set; }
        public string NoExtJaade { get; set; }
        public string LocalidadJaade { get; set; }  
        public string MunicipioJaade { get; set; }
        public string EstadoJaade { get; set; }
        public string TitularVentaJaade { get; set; }
        public string PagoInicialLetra { get; set; }
        public decimal MontoMensualidad { get; set; }
        public string MontoMensualidadLetra { get; set; }
        public decimal TotalContrato { get; set; }
        public string TotalContratoLetra { get; set; }  
        public string UbicacionZona {  get; set; }
        public int? NoManzana { get; set; }
    }
}
