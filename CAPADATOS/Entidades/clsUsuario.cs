using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsUsuario
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string Contrasena { get; set; }
        public string Nombre { get; set; }  
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Curp { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Calle { get; set; }
        public string NoExt { get; set; }
        public string NoInt { get; set; }
        public string Colonia { get; set; }
        public string Localidad { get; set; }
        public string CodigoPostal { get; set; }
        public int RolId { get; set; }
        public string Rol { get; set; }
        public int EstadoId { get; set; }
        public string Estado { get; set; }               
        public DateTime FechaRegistro { get; set; }

    }
}
