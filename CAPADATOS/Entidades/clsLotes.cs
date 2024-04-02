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
        public int ZonaId { get; set; }
        public string Zona { get; set; }
        public int? Manzana { get; set; }
        public string NoLote { get; set; }
        public decimal Precio { get; set; }
        public int EstadoId { get; set; }
        public string Estado { get; set; }  
        public DateTime FechaRegistro { get; set; }
             
    
  
    }
}
