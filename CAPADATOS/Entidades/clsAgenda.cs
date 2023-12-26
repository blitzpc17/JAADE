using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsAgenda
    {
        public int Id { get; set; } 
        public string Tipo { get; set; }
        public int TipoId { get; set; }
        public string Dato { get; set; }
        public int ClienteId { get; set; }



    }
}
