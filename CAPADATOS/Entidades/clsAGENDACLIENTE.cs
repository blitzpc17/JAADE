using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsAGENDACLIENTE
    {
        public int Id { get; set; }
        public int TipoId { get; set; }
        public string Tipo { get; set; }
        public string Valor { get; set; }
        public int RelacionId { get; set; }
    }
}
