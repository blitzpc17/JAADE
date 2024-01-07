using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.ADO.SISTEMA
{
    public class VariableGlobalADO : IDisposable
    {
        private DB_JAADEEntities contexto;

        public VariableGlobalADO()
        {
            contexto = new DB_JAADEEntities();
        }

        public void Insertar(VARIABLEGLOBAL entidad)
        {
            contexto.VARIABLEGLOBAL.Add(entidad);
        }

        public void Guardar()
        {
            contexto.SaveChanges();
        }

        public void Eliminar(VARIABLEGLOBAL entidad)
        {
            contexto.VARIABLEGLOBAL.Remove(entidad);
        }

        public List<VARIABLEGLOBAL> Listar()
        {
            return contexto.VARIABLEGLOBAL.ToList();
        }

        public dynamic Obtener(string nombre)
        {
            var variable = contexto.VARIABLEGLOBAL.FirstOrDefault(x => x.Nombre.Equals(nombre));
            return variable != null ? variable.Valor : null;
        }

        public void Dispose()
        {
            contexto.Dispose();
        }
    }
}
