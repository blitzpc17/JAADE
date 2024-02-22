using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsLoteImportacion
    {
        public int Id { get; set; }
        public string Identificador { get; set; }
        public int ZONAId { get; set; }
        public string Zona { get; set; }
        public int Cantidad { get; set; }
        public decimal MNorte { get; set; }
        public decimal MSur { get; set; }
        public decimal MOeste { get; set; }
        public decimal MEste { get; set; }
        public string CNorte { get; set; }
        public string CSur { get; set; }
        public string COeste { get; set; }
        public string CEste { get; set; }
        public DateTime FechaRegistro { get; set; }
        public decimal Precio { get; set; }
        public int? Manzana { get; set; }
        public int ESTADOId { get; set; }
        
    }
}
