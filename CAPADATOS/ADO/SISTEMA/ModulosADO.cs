using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.ADO.SISTEMA
{
    public class ModulosADO
    {
        private DB_JAADEEntities contexto;

        public ModulosADO()
        {
            contexto = new DB_JAADEEntities();
        }

        public void Insertar(MODULO entidad)
        {
            contexto.MODULO.Add(entidad);
        }

        public void Guardar()
        {
            contexto.SaveChanges();
        }

        public void Eliminar(MODULO entidad)
        {
            contexto.MODULO.Remove(entidad);
        }

        public List<MODULO> Listar()
        {
            return contexto.MODULO.ToList();
        }

        public MODULO Obtener(int id)
        {
            return contexto.MODULO.FirstOrDefault(x => x.Id == id);
        }

        public clsModulo ObtenerModulo(int id)
        {
            var query = "SELECT " +
                       "modu.Id, modu.Nombre, modu.Ruta, modu.Icono, pad.Id as ModuloPadreId, pad.Nombre as ModuloPadre \r\n" +
                       "FROM MODULO AS modu \r\n" +
                       "LEFT JOIN MODULO AS pad ON modu.MODULOId = pad.Id \r\n" +
                       "WHERE modu.Id = "+id;


            return contexto.Database.SqlQuery<clsModulo>(query).FirstOrDefault();
        }

        public List<clsModulo> ListarModulos()
        {
            var query = "SELECT \r\n"+
                        "modu.Id, modu.Nombre, modu.Ruta, modu.Icono, pad.Id as ModuloPadreId, pad.Nombre as ModuloPadre \r\n"+
                        "FROM MODULO AS modu \r\n"+
                        "LEFT JOIN MODULO AS pad ON modu.MODULOId = pad.Id";


            return contexto.Database.SqlQuery<clsModulo>(query).ToList();
        }


    }
}
