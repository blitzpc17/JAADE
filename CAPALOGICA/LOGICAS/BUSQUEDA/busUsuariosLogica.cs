using CAPADATOS.ADO.SISTEMA;
using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPALOGICA.LOGICAS.BUSQUEDA
{
    public class busUsuariosLogica
    {
        private UsuariosADO contexto;
        public List<clsUsuario> LstUsuario;
        public List<clsUsuario> LstUsuarioAux;

        public int index = -1;
        public int indexAux = -1;
        public int Column = 0;

        public busUsuariosLogica()
        {
            contexto = new UsuariosADO();
        }

        public void ListarRegistros()
        {
            LstUsuario = contexto.ListarUsuarios();
        }

        public clsUsuario ObtenerRegistro(int id)
        {
            return contexto.ObtenerDataUsuario(id); 
        }

        public bool Filtrar(int column, string termino)
        {
            if (LstUsuarioAux == null) LstUsuarioAux = LstUsuario;

            switch (column)
            {
                case 1:
                    index = LstUsuarioAux.FindIndex(x => x.Alias.StartsWith(termino));
                    break;
                case 3:
                    index = LstUsuarioAux.FindIndex(x => x.Nombre.ToString().StartsWith(termino));
                    break;
                case 15:
                    index = LstUsuarioAux.FindIndex(x => x.Rol.StartsWith(termino));
                    break;
                case 16:
                    index = LstUsuarioAux.FindIndex(x => x.Estado.StartsWith(termino));
                    break;


                default:
                    index = -1;
                    break;

            }

            return (index >= 0);

        }

        public void Ordenar(int column)
        {
            switch (column)
            {

                case 1:
                    LstUsuarioAux = LstUsuario.OrderBy(x => x.Alias).ThenBy(x => x.Nombre).ThenBy(x => x.Rol).ThenBy(x => x.Estado).ToList();
                    break;
                case 3:
                    LstUsuarioAux = LstUsuario.OrderBy(x => x.Nombre).ThenBy(x => x.Rol).ThenBy(x => x.Estado).ThenBy(x => x.Nombre).ToList();
                    break;
                case 15:
                    LstUsuarioAux = LstUsuario.OrderBy(x => x.Rol).ThenBy(x => x.Estado).ThenBy(x => x.Alias).ThenBy(x => x.Nombre).ToList();
                    break;
                case 16:
                    LstUsuarioAux = LstUsuario.OrderBy(x => x.Estado).ThenBy(x => x.Alias).ThenBy(x => x.Nombre).ThenBy(x => x.Rol).ToList();
                    break;

                default:
                    LstUsuarioAux = LstUsuario.OrderBy(x => x.Alias).ThenBy(x => x.Nombre).ThenBy(x => x.Rol).ThenBy(x => x.Estado).ToList();
                    break;

            }
        }




    }
}
