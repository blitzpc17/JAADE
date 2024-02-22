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

        public bool Guardar()
        {
            try
            {
                contexto.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
           
        }

        public void InsertarMasivo(List<CLIENTE>lst)
        {
            contexto.CLIENTE.AddRange(lst);
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

        public CLIENTE ObtenerXClave(string clave)
        {
            return contexto.CLIENTE.FirstOrDefault(x => x.Clave == clave);
        }

        public clsClientes ObtenerDataCliente(int id)
        {
            var query = "SELECT us.Id, us.Clave, (per.Nombres+' '+per.Apellidos) as Cliente, \r\n"+
                        "per.Nombres, per.Apellidos as Apellidos, \r\n"+
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
            var query = "SELECT us.Id, us.Clave, (per.Nombres+' '+per.Apellidos) as Cliente, \r\n" +
                        "per.Nombres, per.Apellidos as Apellidos, \r\n" +
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

        public clsClientes ObtenerDataClienteClave(string clave)
        {
            var query = "SELECT us.Id, us.Clave, (per.Nombres+' '+per.Apellidos) as Cliente, \r\n" +
                        "per.Nombres, per.Apellidos as Apellidos, \r\n" +
                        "per.Curp, per.FechaNacimiento, per.Calle, per.NoExt, per.NoInt, per.Colonia, per.Localidad, \r\n" +
                        "per.CodigoPostal, edo.Id as EstadoId, edo.Nombre as Estado, \r\n" +
                        "per.EntidadFederativa, per.Municipio \r\n" +
                        "FROM CLIENTE AS us \r\n" +
                        "JOIN PERSONA AS per ON us.PERSONAId = per.Id \r\n" +
                        "JOIN ESTADO AS edo ON us.ESTADOId = edo.Id \r\n" +
                        "WHERE us.Clave = '" +clave +"'";

            return contexto.Database.SqlQuery<clsClientes>(query).FirstOrDefault();
        }
    }
}
