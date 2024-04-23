using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsValidacionContrato
    {
        public int EstadoId { get; set; }
        public string Mensaje { get; set; }
        public bool ProcedePagar {  get; set; }
    }
}
