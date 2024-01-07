using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.ADO.SISTEMA
{
    public class ExcepcionADO:IDisposable
    {
        private DB_JAADEEntities contexto;

        public ExcepcionADO()
        {
            contexto = new DB_JAADEEntities();
        }

        public void Insertar(EXCEPCION entidad)
        {            
            contexto.EXCEPCION.Add(entidad);
        }

        public void Guardar()
        {
            contexto.SaveChanges();
        }

        public void Eliminar(EXCEPCION entidad)
        {
            contexto.EXCEPCION.Remove(entidad);
        }

        public List<EXCEPCION> Listar()
        {
            return contexto.EXCEPCION.ToList();
        }

        public EXCEPCION Obtener(int id)
        {
            return contexto.EXCEPCION.FirstOrDefault(x => x.Id == id);
        }

        public clsExcepciones ObtenerDataExcepcion(int id)
        {
            var query = "select \r\n" +
                        "ex.Id, ex.Fecha, ex.Formulario, ex.Resumen, ex.Detalle, ex.USUARIOId, \r\n" +
                        "(per.Nombres + ' ' + per.ApellidoPaterno + ' ' + per.ApellidoMaterno) as NombreUsuario \r\n" +
                        "from EXCEPCION ex  \r\n" +
                        "join USUARIO us on ex.USUARIOId = us.Id  \r\n" +
                        "join PERSONA per on us.PERSONAId = per.Id  \r\n" +
                        "where ex.Id = "+id+
                        " \r\norder by ex.Fecha desc";

            return contexto.Database.SqlQuery<clsExcepciones>(query).FirstOrDefault();
        }

        public List<clsExcepciones> ListarExcepciones(DateTime fecha)
        {
            var query = "select \r\n" +
                        "ex.Id, ex.Fecha, ex.Formulario, ex.Resumen, ex.Detalle, ex.USUARIOId, \r\n" +
                        "(per.Nombres + ' ' + per.ApellidoPaterno + ' ' + per.ApellidoMaterno) as NombreUsuario \r\n" +
                        "from EXCEPCION ex  \r\n" +
                        "join USUARIO us on ex.USUARIOId = us.Id  \r\n" +
                        "join PERSONA per on us.PERSONAId = per.Id  \r\n" +
                        "where CAST(ex.fecha AS DATE) = @fecha \r\n"+
                        "order by ex.Fecha desc";

            return contexto.Database.SqlQuery<clsExcepciones>(query, new SqlParameter("@Fecha", fecha.Date)).ToList();
        }

        public void Dispose()
        {
            contexto.Dispose();
        }




    }
}
