using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsLotes
    {
        public int Id { get; set; } 
        public string Identificador { get; set; }
        public string Zona { get; set; }
        public string Estado { get; set; }
        public int ZonaId { get; set; }
        public decimal MNorte { get; set; }
        public decimal MSur { get; set; }
        public decimal MEste { get; set; }
        public decimal MOeste { get; set; }
        public string CNorte { get; set; }
        public string CSur { get; set; }
        public string CEste { get; set; }
        public string COeste { get; set; }
        public DateTime FechaRegistro { get; set; }
        public decimal Precio { get; set; }
        public int? Manzana { get; set; }   
        public int EstadoId { get; set; }
     





    }
}
