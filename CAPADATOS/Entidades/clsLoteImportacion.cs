using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsLoteImportacion
    {
        public string Zona { get; set; }
        public int ZonaId {  get; set; }
        public int? Manzana { get; set; }
        public int? NoLote { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        
    }
}
