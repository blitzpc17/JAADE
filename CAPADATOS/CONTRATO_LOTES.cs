//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CAPADATOS
{
    using System;
    using System.Collections.Generic;
    
    public partial class CONTRATO_LOTES
    {
        public int Id { get; set; }
        public int CONTRATOId { get; set; }
        public int LOTEId { get; set; }
    
        public virtual CONTRATO CONTRATO { get; set; }
        public virtual LOTE LOTE { get; set; }
    }
}
