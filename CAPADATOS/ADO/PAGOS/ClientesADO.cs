using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.ADO.PAGOS
{
    public class ClientesADO:IDisposable
    {

        private DB_JAADEEntities contexto;

        public ClientesADO()
        {
            contexto = new DB_JAADEEntities();
        }

        public void Insertar(CLIENTE entidad)
        {
            contexto.CLIENTE.Add(entidad);
        }

        public void Guardar()
        {
            contexto.SaveChanges();
        }

        public void Eliminar(CLIENTE entidad)
        {
            contexto.CLIENTE.Remove(entidad);
        }

        public List<CLIENTE> Listar()
        {
            return contexto.CLIENTE.ToList();
        }

        public CLIENTE Obtener(int id)
        {
            return contexto.CLIENTE.FirstOrDefault(x => x.Id == id);
        }

        public clsClientes ObtenerDataCliente(int id)
        {
            var query = "SELECT us.Id, us.Clave, (per.Nombres+' '+per.ApellidoPaterno+' '+per.ApellidoMaterno) as Cliente, \r\n"+
                        "per.Nombres, per.ApellidoPaterno as Apaterno, per.ApellidoMaterno as Amaterno, \r\n"+
                        "per.Curp, per.FechaNacimiento, per.Calle, per.NoExt, per.NoInt, per.Colonia, per.Localidad, \r\n"+
                        "per.CodigoPostal, edo.Id as EstadoId, edo.Nombre as Estado, \r\n"+
                        "per.EntidadFederativa, per.Municipio \r\n"+
                        "FROM CLIENTE AS us \r\n"+
                        "JOIN PERSONA AS per ON us.PERSONAId = per.Id \r\n"+
                        "JOIN ESTADO AS edo ON us.ESTADOId = edo.Id \r\n"+
                        "WHERE us.id = " + id;

            return contexto.Database.SqlQuery<clsClientes>(query).FirstOrDefault();
        }

        public List<clsClientes> ListarClientes()
        {
            var query = "SELECT us.Id, us.Clave, (per.Nombres+' '+per.ApellidoPaterno+' '+per.ApellidoMaterno) as Cliente, \r\n" +
                        "per.Nombres, per.ApellidoPaterno as Apaterno, per.ApellidoMaterno as Amaterno, \r\n" +
                        "per.Curp, per.FechaNacimiento, per.Calle, per.NoExt, per.NoInt, per.Colonia, per.Localidad, \r\n" +
                        "per.CodigoPostal, edo.Id as EstadoId, edo.Nombre as Estado, \r\n" +
                        "per.EntidadFederativa, per.Municipio \r\n" +
                        "FROM CLIENTE AS us \r\n" +
                        "JOIN PERSONA AS per ON us.PERSONAId = per.Id \r\n"+
                        "JOIN ESTADO AS edo ON us.ESTADOId = edo.Id ";

            return contexto.Database.SqlQuery<clsClientes>(query).ToList();
        }

        public void Dispose()
        {
            contexto.Dispose();
        }
    }
}
