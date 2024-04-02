using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.ADO.SISTEMA
{
    public class RolPermisoADO : IDisposable
    {
        private DB_JAADEEntities contexto;

        public RolPermisoADO()
        {
            contexto = new DB_JAADEEntities();
        }

        public ROL_PERMISO Insertar(ROL_PERMISO obj)
        {
            return contexto.ROL_PERMISO.Add(obj);
        }

        public ROL_PERMISO Obtener(int idPermiso)
        {
            return contexto.ROL_PERMISO.FirstOrDefault(x=>x.Id == idPermiso);
        }

        public void Eliminar(ROL_PERMISO obj)
        {
            contexto.ROL_PERMISO.Remove(obj);
        }

        public void Guardar()
        {
            contexto.SaveChanges(); 
        }


        public void Dispose()
        {
            contexto?.Dispose();
        }

        public bool ValidarExistePermisoEnRol(int rolId, int moduloId)
        {
            return contexto.ROL_PERMISO.Any(x => x.ROLId == rolId && x.MODULOId == moduloId);
        }

    }
}
