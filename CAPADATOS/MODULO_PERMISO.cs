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
    
    public partial class MODULO_PERMISO
    {
        public int Id { get; set; }
        public string FechaRegistro { get; set; }
        public int MODULOId { get; set; }
        public int USUARIOId { get; set; }
        public string Motivo { get; set; }
        public int USUARIOAUTORIZOId { get; set; }
    
        public virtual MODULO MODULO { get; set; }
        public virtual USUARIO USUARIO { get; set; }
        public virtual USUARIO USUARIOAUTORIZO { get; set; }
    }
}
