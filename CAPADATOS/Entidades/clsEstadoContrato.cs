using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsEstadoContrato
    {
        public string NombreEstado { get; set; }
        public int EstadoId { get; set; }
        public decimal? MontoExtendido { get; set; }
        public decimal? MontoPagoExtendido { get; set; }
        public DateTime? FechaInicioProrroga { get; set; }
        public bool? Reubicado { get; set; }


    }
}
