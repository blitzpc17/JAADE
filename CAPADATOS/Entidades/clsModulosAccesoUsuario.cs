using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsModulosAccesoUsuario
    {
        public int ModuloId { get; set; }
        public string Nombre { get; set; }  
        public string Icono { get; set; }
        public string Ruta { get; set; }
        public int ModuloSubId { get; set; }
        public string ModuloSubNombre { get; set; }
        public string ModuloSubRuta { get; set; }
        public int ModuloPadreId { get; set; }
        public string ModuloPadreNombre { get; set; }
        public string ModuloPadreRuta { get; set; }

    }
}
