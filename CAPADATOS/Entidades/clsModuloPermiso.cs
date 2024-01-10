using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsModuloPermiso
    {
        public int PermisoId { get; set; }
        public int ModuloId { get; set; }
        public string NombreModulo { get; set; }
        public string RutaModulo { get; set; }
        public int UsuarioAsignoId { get; set; }
        public string NombreUsuarioAsigno { get; set; }
        public DateTime FechaAsigno { get; set; }
        public int UsuarioSolicitaId { get; set; }
        public string NombreUsuarioSolicita { get; set; }
        public string Motivo { get; set; }
      
    }
}
