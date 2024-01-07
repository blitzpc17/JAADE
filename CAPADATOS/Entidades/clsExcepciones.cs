using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsExcepciones
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }       
        public string Formulario { get; set; }
        public string Resumen { get; set; }
        public string Detalle { get; set; }
        public int USUARIOId { get; set; }
        public string NombreUsuario { get; set; }
    }
}
