using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.ADO.PAGOS
{
    public class ClienteSocioADO:IDisposable
    {
        private DB_JAADEEntities contexto;
        public ClienteSocioADO()
        {
            contexto = new DB_JAADEEntities();
        }

        public void Insertar(CLIENTES_SOCIOS entidad)
        {
            contexto.CLIENTES_SOCIOS.Add(entidad);
        }

        public bool Guardar()
        {
            try
            {
                contexto.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public void Eliminar(CLIENTES_SOCIOS entidad)
        {
            contexto.CLIENTES_SOCIOS.Remove(entidad);
        }

        public List<CLIENTES_SOCIOS> Listar()
        {
            return contexto.CLIENTES_SOCIOS.ToList();
        }

        public CLIENTES_SOCIOS Obtener(int socioId)
        {
            return contexto.CLIENTES_SOCIOS.FirstOrDefault(x => x.SOCIOSId == socioId);
        }
        public void Dispose()
        {
            contexto.Dispose();
        }
    }
}
