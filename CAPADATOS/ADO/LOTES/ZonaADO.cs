using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.ADO.LOTES
{
    public class ZonaADO
    {

        private DB_JAADEEntities contexto;

        public ZonaADO()
        {
            contexto = new DB_JAADEEntities();
        }

        public void Insertar(ZONA entidad)
        {
            contexto.ZONA.Add(entidad);
        }

        public void Guardar()
        {
            contexto.SaveChanges();
        }

        public void Eliminar(ZONA entidad)
        {
            contexto.ZONA.Remove(entidad);
        }

        public List<ZONA> Listar()
        {
            return contexto.ZONA.ToList();
        }

        public ZONA Obtener(int id)
        {
            return contexto.ZONA.FirstOrDefault(x => x.Id == id);
        }

        public ZONA ObtenerZonaNombre(string nombreZona)
        {
            return contexto.ZONA.FirstOrDefault(x => x.Nombre == nombreZona);
        }
    }
}
