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
    public class formPermisoRolLogica
    {
        private RolesADO contextoRol;
        private ModuloPermisoADO contextoPermiso;
        private ModulosADO contextoModulos;
        private RolPermisoADO contextoRolPermiso;

        public List<clsRolPermiso> LstPermisos;
        public List<clsRolPermiso> LstPermisosAux;
        public List<ROL> LstRol;     
        public List<clsUsuario> LstUsuario;
        public List<int> LstUsuariosIdPermiso;

        public clsUsuario ObjUsuario;
        public clsModuloPermiso ObjPermisoData;
        public clsModulo ObjModulo;
        public MODULO_PERMISO ObjPermiso;
        public ROL_PERMISO ObjRolPermiso;

        public int RolId;

        public int index = -1;
        public int indexAux = -1;
        public int Column = 0;


        public formPermisoRolLogica()
        {
            contextoRol = new RolesADO(); 
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

        public void ListarPermisosXRol()
        {
            if (RolId == -1) return;
            LstPermisos = contextoPermiso.ListarPermisosXRol(RolId);
            LstPermisosAux = LstPermisos;
        }


        public clsModulo ObtenerDataModulo(int moduloId)
        {
            return contextoModulos.ObtenerModulo(moduloId);
        }

        public bool EliminarPermisoXRol(MODULO_PERMISO obj,  List<int> lstUsuarios)
        {
            return contextoPermiso.EliminarPermisoXRol(obj, lstUsuarios);
        }

        public bool InsertarPermisoMasivoRol(MODULO_PERMISO obj,  List<int>lstids)
        {
            return contextoPermiso.InsertarPermisoXRol(obj, lstids);
        }

        public void ListarRoles()
        {
            LstRol = contextoRol.Listar();
        }

        public bool ValidarPermisoEnUsuarios(int moduloId, int rolId, bool tienen = false)
        {
            return contextoRolPermiso.ValidarExistePermisoEnRol(rolId, moduloId);
        }


        public bool Filtrar(int column, string termino)
        {
            if (LstPermisosAux == null) LstPermisosAux = LstPermisos;

            switch (column)
            {
                case 1:
                    index = LstPermisosAux.FindIndex(x => x.NombreModulo.StartsWith(termino));
                    break;
                case 2:
                    index = LstPermisosAux.FindIndex(x => x.NombreModulo.ToString().StartsWith(termino));
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
                    LstPermisosAux = LstPermisos.OrderBy(x => x.NombreModulo).ThenBy(x => x.RutaModulo).ToList();
                    break;
                case 2:
                    LstPermisosAux = LstPermisos.OrderBy(x => x.RutaModulo).ThenBy(x => x.NombreModulo).ToList();
                    break;

                default:
                    LstPermisosAux = LstPermisos.OrderBy(x => x.NombreModulo).ThenBy(x => x.RutaModulo).ToList();
                    break;

            }
        }

        public void CrearPermisoRol()
        {
            ObjRolPermiso = new ROL_PERMISO();
        }

        public void GuardarPermisoRol()
        {
            contextoRolPermiso.Insertar(ObjRolPermiso);
            contextoRolPermiso.Guardar();
        }

        public bool ValidarExistePermisoEnRol(int rolId, int ModuloId)
        {
            return contextoRolPermiso.ValidarExistePermisoEnRol(rolId, ModuloId);
        }
    }
}
