using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.ADO.PAGOS
{
    public class SociosADO : IDisposable
    {
        private DB_JAADEEntities contexto;

        public SociosADO()
        {
            contexto = new DB_JAADEEntities();
        }

        public void Insertar(SOCIOS entidad)
        {
            contexto.SOCIOS.Add(entidad);
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

        public void Eliminar(SOCIOS entidad)
        {
            contexto.SOCIOS.Remove(entidad);
        }

        public List<SOCIOS> Listar()
        {
            return contexto.SOCIOS.ToList();
        }

        public SOCIOS Obtener(int id)
        {
            return contexto.SOCIOS.FirstOrDefault(x => x.Id == id);
        }

        public SOCIOS BuscarSociosPorNombreClienteId(int clienteId, string nombre)
        {
            string sql = "SELECT SO.* FROM CLIENTE CLI "+
                            "JOIN CLIENTES_SOCIOS CLISO ON CLI.Id = CLISO.CLIENTEId "+
                            "JOIN SOCIOS SO ON CLISO.SOCIOSId = SO.Id "+
                            "WHERE CLI.Id = "+clienteId+" AND SO.Nombre = '"+nombre+"'";
            return contexto.Database.SqlQuery<SOCIOS>(sql).FirstOrDefault();
        }

        public void Dispose()
        {
            contexto.Dispose();
        }

        public List<SOCIOS> ListarClientesSocios(string clave)
        {
            string query = "SELECT SO.* FROM CLIENTE AS CLI \r\n" +
                             "JOIN CLIENTES_SOCIOS CS ON CLI.Id = CS.CLIENTEId \r\n"+
                             "JOIN SOCIOS SO ON CS.SOCIOSId = SO.Id \r\n"+
                             "WHERE CLI.Clave = '"+clave+"';";
            return contexto.Database.SqlQuery<SOCIOS>(query).ToList();
        }
    }
}
