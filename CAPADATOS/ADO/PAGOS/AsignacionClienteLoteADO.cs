using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.ADO.PAGOS
{
    public class AsignacionClienteLoteADO:IDisposable
    {
        private DB_JAADEEntities contexto;

        public AsignacionClienteLoteADO()
        {
            contexto = new DB_JAADEEntities();
        }

        public void Insertar(CLIENTELOTE entidad)
        {
            contexto.CLIENTELOTE.Add(entidad);
        }

        public void Guardar()
        {
            contexto.SaveChanges();
        }

        public void Eliminar(CLIENTELOTE entidad)
        {
            contexto.CLIENTELOTE.Remove(entidad);
        }

        public List<CLIENTELOTE> Listar()
        {
            return contexto.CLIENTELOTE.ToList();
        }

        public CLIENTELOTE Obtener(int id)
        {
            return contexto.CLIENTELOTE.FirstOrDefault(x => x.Id == id);
        }

        public clsClienteLote ObtenerAsignacion(int id)
        {
            var query = " " + id;

            return contexto.Database.SqlQuery<clsClienteLote>(query).FirstOrDefault();
        }

        public List<clsClienteLote> ListarAsignacionesClientes(int clienteId)
        {
            string query = "SELECT " +
                           "C.Id as ClienteId, zn.Id as ZonaId, LT.Id as LoteId, \r\n" +
                           "LT.Identificador as CodigoLote, ZN.Nombre AS ZonaNombre,  \r\n" +
                           "(per.Nombres+' '+per.ApellidoPaterno+' '+per.ApellidoMaterno) as Cliente, \r\n" +
                           "CL.FechaRegistro as FechaAsignacion, \r\n" +
                           "LT.Manzana, LT.Precio as PrecioLote, CL.NoPagos, CL.MontoRestante, \r\n" +
                           "EDO.Id as EstadoId, EDO.Nombre as Estado, \r\n "+
                           "(SELECT COUNT(*) FROM PAGO WHERE PAGO.CLIENTEId = C.Id AND PAGO.LOTEId = LT.Id) as  PagosRegistrados, \r\n" +
                           "CL.Id as Id \r\n"+
                            "FROM \r\n" +
                            "CLIENTE_LOTE CL \r\n" +
                            "JOIN CLIENTE C ON CL.CLIENTEId = C.Id \r\n" +
                            "JOIN PERSONA AS per ON C.PERSONAId = per.Id \r\n" +
                            "JOIN LOTE LT ON CL.LOTEId = LT.Id \r\n" +
                            "JOIN ZONA ZN ON LT.ZONAId = ZN.Id \r\n" +
                            "JOIN ESTADO EDO ON LT.ESTADOId = EDO.Id \r\n"+
                            "WHERE C.Id = " + clienteId;

            return contexto.Database.SqlQuery<clsClienteLote>(query).ToList();

           
        }

        public clsClienteLote ObtenerAsignacionesLotes(int clienteId, int loteId)
        {
            string query =  "SELECT " +
                            "C.Id as ClienteId, zn.Id as ZonaId, LT.Id as LoteId, \r\n" +
                            "LT.Identificador as CodigoLote, ZN.Nombre AS ZonaNombre,  \r\n" +
                            "(per.Nombres+' '+per.ApellidoPaterno+' '+per.ApellidoMaterno) as Cliente, \r\n" +
                            "CL.FechaRegistro as FechaAsignacion, \r\n" +
                            "LT.Manzana, LT.Precio as PrecioLote, CL.NoPagos, CL.MontoRestante, \r\n" +
                            "(SELECT COUNT(*) FROM PAGO WHERE PAGO.CLIENTEId = C.Id AND PAGO.LOTEId = LT.Id) as PagosRegistrados, \r\n" +
                            "CL.Id as Id \r\n" +
                            "FROM \r\n" +
                            "CLIENTE_LOTE CL \r\n" +
                            "JOIN CLIENTE C ON CL.CLIENTEId = C.Id \r\n" +
                            "JOIN PERSONA AS per ON C.PERSONAId = per.Id \r\n" +
                            "JOIN LOTE LT ON CL.LOTEId = LT.Id \r\n" +
                            "JOIN ZONA ZN ON LT.ZONAId = ZN.Id \r\n" +
                            "WHERE C.Id = " + clienteId + " AND LT.Id = "+loteId;

            return contexto.Database.SqlQuery<clsClienteLote>(query).FirstOrDefault();

        }

        public CLIENTELOTE ObtenerAsignacionClienteLote(int idCliente, int idLote)
        {
            return contexto.CLIENTELOTE.FirstOrDefault(x=>x.CLIENTEId == idCliente && x.LOTEId == idLote);
        }

        public void Dispose()
        {
            contexto.Dispose();
        }
    }
}
