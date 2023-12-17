using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsModulo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public int? ModuloPadreId { get; set; }
        public string ModuloPadre { get; set; }
        public string Icono { get; set; }   
    }
}
