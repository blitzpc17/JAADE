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
    
    public partial class CLIENTES_SOCIOS
    {
        public int Id { get; set; }
        public int CLIENTEId { get; set; }
        public int SOCIOSId { get; set; }
    
        public virtual CLIENTE CLIENTE { get; set; }
        public virtual SOCIOS SOCIOS { get; set; }
    }
}