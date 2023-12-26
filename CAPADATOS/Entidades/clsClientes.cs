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
        public string Apaterno { get; set; }
        public string Amaterno { get; set; }
        public string Curp { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Calle { get; set; }
        public string NoExt { get; set; }
        public string NoInt { get; set; }
        public string Colonia { get; set; }
        public string Localidad { get; set; }
        public string CodigoPostal { get; set; }     
        public int EstadoId { get; set; }
        public string Estado { get; set; }




    }
}
