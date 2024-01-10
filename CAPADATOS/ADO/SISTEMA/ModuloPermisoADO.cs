using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.ADO.SISTEMA
{
    public class ModuloPermisoADO:IDisposable
    {
        private DB_JAADEEntities contexto;

        public ModuloPermisoADO()
        {
            contexto = new DB_JAADEEntities();
        }

        public void Insertar(MODULO_PERMISO entidad)
        {
            contexto.MODULO_PERMISO.Add(entidad);
        }

        public void Guardar()
        {
            contexto.SaveChanges();
        }

        public void Eliminar(MODULO_PERMISO entidad)
        {
            contexto.MODULO_PERMISO.Remove(entidad);
        }

        public List<MODULO_PERMISO> Listar()
        {
            return contexto.MODULO_PERMISO.ToList();
        }

        public MODULO_PERMISO Obtener(int id)
        {
            return contexto.MODULO_PERMISO.FirstOrDefault(x => x.Id == id);
        }

        public List<clsModuloPermiso>ListarModuloPermisoUsuario(int idUsuario)
        {
            string query = "SELECT \r\n " +
                            "MP.Id AS PermisoId, USAUT.Id as UsuarioAsignoId, \r\n " +
                            "(PERAUT.Nombres + ' ' + PERAUT.ApellidoPaterno + ' ' + PERAUT.ApellidoMaterno) as NombreUsuarioAsigno, \r\n " +
                            "MP.FechaRegistro as FechaAsigno, USSOL.Id as UsuarioSolicitaId, \r\n " +
                            "(PERSOL.Nombres + ' ' + PERSOL.ApellidoPaterno + ' ' + PERSOL.ApellidoMaterno) as NombreUsuarioSolicita, \r\n " +
                            "MP.Motivo, M.MODULOId as ModuloId, M.Nombre as NombreModulo, M.Ruta as RutaModulo \r\n " +
                            "FROM MODULO_PERMISO AS MP \r\n " +
                            "JOIN MODULO AS M ON MP.MODULOId = M.Id \r\n " +
                            "JOIN USUARIO AS USSOL ON MP.USUARIOId = USSOL.Id \r\n " +
                            "JOIN PERSONA AS PERSOL ON USSOL.PERSONAId = PERSOL.Id \r\n " +
                            "JOIN USUARIO AS USAUT ON MP.USUARIOAUTORIZOId = USAUT.Id \r\n " +
                            "JOIN PERSONA AS PERAUT ON USAUT.PERSONAId = PERAUT.Id \r\n " +
                            "WHERE USSOL.Id = "+idUsuario;

            return contexto.Database.SqlQuery<clsModuloPermiso>(query).ToList();
        }

        public clsModuloPermiso ObtenerModuloPermisoUsuario(int idUsuario, int permisoId)
        {
            string query = "SELECT \r\n "+
                            "MP.Id AS PermisoId, USAUT.Id as UsuarioAsignoId, \r\n "+
                            "(PERAUT.Nombres + ' ' + PERAUT.ApellidoPaterno + ' ' + PERAUT.ApellidoMaterno) as NombreUsuarioAsigno, \r\n "+
                            "MP.FechaRegistro as FechaAsigno, USSOL.Id as UsuarioSolicitaId, \r\n "+
                            "(PERSOL.Nombres + ' ' + PERSOL.ApellidoPaterno + ' ' + PERSOL.ApellidoMaterno) as NombreUsuarioSolicita, \r\n "+
                            "MP.Motivo, M.MODULOId as ModuloId, M.Nombre as NombreModulo, M.Ruta as RutaModulo \r\n "+
                            "FROM MODULO_PERMISO AS MP \r\n "+
                            "JOIN MODULO AS M ON MP.MODULOId = M.Id \r\n "+
                            "JOIN USUARIO AS USSOL ON MP.USUARIOId = USSOL.Id \r\n "+
                            "JOIN PERSONA AS PERSOL ON USSOL.PERSONAId = PERSOL.Id \r\n "+
                            "JOIN USUARIO AS USAUT ON MP.USUARIOAUTORIZOId = USAUT.Id \r\n "+
                            "JOIN PERSONA AS PERAUT ON USAUT.PERSONAId = PERAUT.Id " +
                            "WHERE USSOL.Id = "+idUsuario+" AND MP.Id = "+permisoId;

            return contexto.Database.SqlQuery<clsModuloPermiso>(query).FirstOrDefault();     
        }

        public void Dispose()
        {
            contexto.Dispose();
        }
    }
}
