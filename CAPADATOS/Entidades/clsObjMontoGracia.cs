using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsObjMontoGracia
    {
        public int ContratoId { get; set; }
        public string FolioContrato { get; set; }
        public int NoPagosDados { get; set; }
        public decimal MontoAcumuladoDado { get; set; }
        public decimal MontoAnualMinimo { get; set; }
        public decimal MontoGracia { get; set; }
        public int NoPagosGracia { get; set; }
        public decimal MontoMensualGracia { get; set; }
        public decimal PrecioInicial { get; set; }
        public int NoPagoActual { get; set; }   
    }
}
