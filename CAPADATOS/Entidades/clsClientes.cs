using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsClientes
    {

        public int Id { get; set; }
        public string Clave { get; set; }
        public string Cliente { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Curp { get; set; }
        public DateTime? FechaNacimiento { get; set; }      
        public int EstadoId { get; set; }
        public string Estado { get; set; }




    }
}
