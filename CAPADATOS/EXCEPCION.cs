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
    
    public partial class EXCEPCION
    {
        public int Id { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Formulario { get; set; }
        public string Resumen { get; set; }
        public string Detalle { get; set; }
        public int USUARIOId { get; set; }
    
        public virtual USUARIO USUARIO { get; set; }
    }
}
