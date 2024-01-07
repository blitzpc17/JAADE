using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.ADO.SISTEMA
{
    public class UsuariosADO:IDisposable
    {
        private DB_JAADEEntities contexto;

        public UsuariosADO()
        {
            contexto = new DB_JAADEEntities();
        }

        public void Insertar(USUARIO entidad)
        {
            contexto.USUARIO.Add(entidad);
        }

        public void Guardar()
        {
            contexto.SaveChanges();
        }

        public void Eliminar(USUARIO entidad)
        {
            contexto.USUARIO.Remove(entidad);
        }

        public List<USUARIO> Listar()
        {
            return contexto.USUARIO.ToList();
        }

        public USUARIO Obtener(int id)
        {
            return contexto.USUARIO.FirstOrDefault(x => x.Id == id);
        }

        public clsUsuario ObtenerDataUsuario(int id)
        {
            var query = "SELECT " +
                        "us.Id, us.Alias, us.Password as Contrasena, \r\n" +
                        "(per.Nombres + ' ' + per.ApellidoPaterno + ' ' + per.ApellidoMaterno) as Nombre, \r\n" +
                        "per.Nombres, per.ApellidoPaterno as Apaterno, per.ApellidoMaterno as Amaterno, \r\n" +
                        "per.Curp, per.FechaNacimiento, per.Calle, per.NoExt, per.NoInt, per.Colonia, per.Localidad, \r\n" +
                        "per.CodigoPostal, rl.Id as RolId, rl.Nombre as Rol, edo.Id as EstadoId, edo.Nombre as Estado, \r\n" +
                        "us.FechaRegistro \r\n" +
                        "FROM USUARIO AS us \r\n" +
                        "JOIN PERSONA AS per ON us.PERSONAId = per.Id \r\n" +
                        "JOIN ESTADO AS edo ON us.ESTADOId = edo.Id \r\n" +
                        "JOIN ROL AS rl ON us.ROLId = rl.Id \r\n" +
                        "WHERE us.id = "+id;

            return contexto.Database.SqlQuery<clsUsuario>(query).FirstOrDefault();
        }

        public List<clsUsuario> ListarUsuarios()
        {
            var query = "SELECT "+
                        "us.Id, us.Alias, us.Password as Contrasena, \r\n"+
                        "(per.Nombres + ' ' + per.ApellidoPaterno + ' ' + per.ApellidoMaterno) as Nombre, \r\n"+
                        "per.Nombres, per.ApellidoPaterno as Apaterno, per.ApellidoMaterno as Amaterno, \r\n"+
                        "per.Curp, per.FechaNacimiento, per.Calle, per.NoExt, per.NoInt, per.Colonia, per.Localidad, \r\n"+
                        "per.CodigoPostal, rl.Id as RolId, rl.Nombre as Rol, edo.Id as EstadoId, edo.Nombre as Estado, \r\n"+
                        "us.FechaRegistro \r\n"+
                        "FROM USUARIO AS us \r\n"+
                        "JOIN PERSONA AS per ON us.PERSONAId = per.Id \r\n"+
                        "JOIN ESTADO AS edo ON us.ESTADOId = edo.Id \r\n"+
                        "JOIN ROL AS rl ON us.ROLId = rl.Id";

            return contexto.Database.SqlQuery<clsUsuario>(query).ToList();   
        }

        public USUARIO Authenticate(string usuario, string password)
        {
            var query = "SELECT TOP 1 *FROM USUARIO WHERE ALIAS = '"+usuario+"' AND PASSWORD = '"+password+"' ";
            return contexto.Database.SqlQuery<USUARIO>(query).FirstOrDefault();
        }

        public void Dispose()
        {
            try
            {
                contexto.Dispose();
            }
            catch
            {
                throw;
            }
        }
    }
}
