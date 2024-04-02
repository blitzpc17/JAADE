using CAPADATOS;
using CAPADATOS.ADO.SISTEMA;
using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPALOGICA.LOGICAS.SISTEMA
{
    public class formPermisoLogica
    {
        private UsuariosADO contextoUsuario;
        private ModuloPermisoADO contextoPermiso;
        private ModulosADO contextoModulos;
        private RolPermisoADO contextoRolPermiso;

        public List<clsModuloPermiso> LstPermisos;
        public List<clsModuloPermiso> LstPermisosAux;
        public clsUsuario ObjUsuario;
        public clsModuloPermiso ObjPermisoData;
        public clsModulo ObjModulo;
        public MODULO_PERMISO ObjPermiso;
        public List<clsUsuario> LstUsuario;

        public int index = -1;
        public int indexAux = -1;
        public int Column = 0;


        public formPermisoLogica()
        {
            contextoUsuario = new UsuariosADO();
            contextoPermiso = new ModuloPermisoADO();
            contextoModulos = new ModulosADO();
            contextoRolPermiso = new RolPermisoADO();
        }

        public void InstanciarPermiso()
        {
            ObjPermiso = new MODULO_PERMISO();    
        }

        public void Guardar()
        {
            if (ObjPermiso.Id == 0)
            {
                contextoPermiso.Insertar(ObjPermiso);
            }
            contextoPermiso.Guardar();

        }

        public MODULO_PERMISO ObtenerPermiso(int id)
        {
            return contextoPermiso.Obtener(id);
        }

        public clsModuloPermiso ObtenerDataPermiso(int idUsuario, int idModulo)
        {
            return contextoPermiso.ObtenerModuloPermisoUsuario(idUsuario, idModulo);
        }

        public void ListarPermisosUsuario(int usuarioId)
        {
            LstPermisos = contextoPermiso.ListarModuloPermisoUsuario(usuarioId);
            LstPermisosAux = LstPermisos;
        }

        public clsUsuario ObtenerDataUsuario(int usuarioId)
        {
            return contextoUsuario.ObtenerDataUsuario(usuarioId);
        }

        public clsModulo ObtenerDataModulo(int moduloId)
        {
            return contextoModulos.ObtenerModulo(moduloId);
        }

        public bool ValidarExistePermisoEnUsuario(clsUsuario objUsuario, int moduloId)
        {
            return contextoPermiso.ValidarPermisoEnUsuarios(objUsuario, moduloId);
        }

        public bool Filtrar(int column, string termino)
        {
            if (LstPermisosAux == null) LstPermisosAux = LstPermisos;

            switch (column)
            {
                case 2:
                    index = LstPermisosAux.FindIndex(x => x.NombreModulo.StartsWith(termino));
                    break;
                case 3:
                    index = LstPermisosAux.FindIndex(x => x.RutaModulo.ToString().StartsWith(termino));
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

                case 2:
                    LstPermisosAux = LstPermisos.OrderBy(x => x.NombreModulo).ThenBy(x => x.RutaModulo).ToList();
                    break;
                case 3:
                    LstPermisosAux = LstPermisos.OrderBy(x => x.RutaModulo).ThenBy(x => x.NombreModulo).ToList();
                    break;              

                default:
                    LstPermisosAux = LstPermisos.OrderBy(x => x.NombreModulo).ThenBy(x => x.RutaModulo).ToList();
                    break;

            }
        }

        public bool EliminarPermiso(int moduloId, int usuarioId)
        {
            return contextoPermiso.EliminarPermiso(moduloId, usuarioId);
        }
    }
}
