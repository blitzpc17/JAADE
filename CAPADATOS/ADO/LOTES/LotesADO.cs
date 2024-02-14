using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.ADO.LOTES
{
    public class LotesADO
    {

        private DB_JAADEEntities contexto;

        public LotesADO()
        {
            contexto = new DB_JAADEEntities();
        }

        public void Insertar(LOTE entidad)
        {
            contexto.LOTE.Add(entidad);
        }

        public void Guardar()
        {
            contexto.SaveChanges();
        }

        public void Eliminar(LOTE entidad)
        {
            contexto.LOTE.Remove(entidad);
        }

        public List<LOTE> Listar()
        {
            return contexto.LOTE.ToList();
        }      

        public List<clsLotes> ListarLotes(int id,bool busquedaCliente = false)
        {
            string query = "";

            if (busquedaCliente)
            {
                query = "select " +
                        "LT.Id, LT.Identificador, ZN.Nombre AS Zona, zn.Id as ZonaId, \r\n " +
                        "LT.MNorte, LT.MSur, LT.MEste, LT.MOeste, LT.CNorte, LT.CSur, \r\n" +
                        "LT.CEste, Lt.COeste, LT.FechaRegistro, LT.Precio, LT.Manzana, \r\n" +
                        "EDO.Id as EstadoId, EDO.Nombre as Estado \r\n"+
                        "FROM CLIENTE_LOTE cl \r\n" +
                        "JOIN CLIENTE cli ON cl.CLIENTEId = cli.Id \r\n" +
                        "JOIN LOTE lt ON cl.LOTEId = lt.Id \r\n" +
                        "JOIN ZONA ZN ON lt.ZONAId = zn.Id \r\n"+
                        "JOIN ESTADO EDO ON lt.ESTADOId = EDO.Id \r\n"+
                        "WHERE cli.Id = "+id;
            }
            else
            {
                query = "SELECT " +
                           "LT.Id, LT.Identificador, ZN.Nombre AS Zona, zn.Id as ZonaId, \r\n" +
                           "LT.MNorte, LT.MSur, LT.MEste, LT.MOeste, LT.CNorte, LT.CSur, \r\n" +
                           "LT.CEste, Lt.COeste, LT.FechaRegistro, LT.Precio, LT.Manzana, \r\n" +
                           "EDO.Id as EstadoId, EDO.Nombre as Estado \r\n" +
                           "FROM ZONA AS ZN " +
                           "JOIN LOTE AS LT ON ZN.Id = LT.ZONAId " +
                           "JOIN ESTADO EDO ON lt.ESTADOId = EDO.Id \r\n" +
                           "WHERE ZN.Id = " + id;
            }
            

            return contexto.Database.SqlQuery<clsLotes>(query).ToList();
        }

        public LOTE Obtener(int id)
        {
            return contexto.LOTE.FirstOrDefault(x => x.Id == id);
        }

        public int ObtenerUltimoLote(int zonaId)
        {            
            return contexto.LOTE.Where(x => x.ZONAId == zonaId).Count();
        }

        public clsLotes ObtenerLoteData(int loteId)
        {
            string query = "SELECT " +
                           "LT.Id, LT.Identificador, ZN.Nombre AS Zona, zn.Id ZonaId, " +
                           "LT.MNorte, LT.MSur, LT.MEste, LT.MOeste, LT.CNorte, LT.CSur, " +
                           "LT.CEste, Lt.COeste, LT.FechaRegistro, LT.Precio, LT.Manzana, \r\n" +
                           "EDO.Id as EstadoId, EDO.Nombre as Estado \r\n" +
                           "FROM ZONA AS ZN " +
                           "JOIN LOTE AS LT ON ZN.Id = LT.ZONAId " +
                           "JOIN ESTADO EDO ON lt.ESTADOId = EDO.Id \r\n" +
                           "WHERE LT.Id = " + loteId;

            return contexto.Database.SqlQuery<clsLotes>(query).FirstOrDefault();
        }

        public LOTE ObtenerLoteIdentificador(string claveLote)
        {
            return contexto.LOTE.FirstOrDefault(x=>x.Identificador == claveLote);
        }

        public clsInformacionPagoLote ObtenerDataPagoLote(int idCliente, int idLote)
        {
            string query = "SELECT \r\n" +
                            "A.*, ((A.PrecioLote - A.PagoInicial) / A.NumeroPagos) AS MontoMensualidad \r\n" +
                            "FROM( \r\n" +
                            "SELECT \r\n" +
                            "Lt.Id as LoteId, LT.Identificador as IdentificadorLote, \r\n" +
                            "LT.Precio as PrecioLote, \r\n" +
                            "CL.FechaRegistro as FechaContrato, \r\n" +
                            "CL.PagoInicial as PagoInicial, \r\n" +
                            "CL.NoPagos as NumeroPagos, CLI.Id as ClienteId, \r\n" +
                            "( \r\n" +
                            "    SELECT COUNT(*)FROM PAGO WHERE PAGO.CLIENTEId = CLI.Id AND PAGO.LOTEId = LT.Id \r\n" +
                            ") as PagosRealizados, \r\n" +
                            "( \r\n" +
                            "    SELECT SUM(PAGO.Monto) FROM PAGO WHERE PAGO.CLIENTEId = CLI.Id AND PAGO.LOTEId = LT.Id \r\n" +
                            ") AS SaldoFavor \r\n" +
                            "FROM CLIENTE_LOTE CL \r\n" +
                            "JOIN CLIENTE CLI ON CL.CLIENTEId = CLI.Id \r\n" +
                            "JOIN LOTE LT ON CL.LOTEId = LT.Id \r\n" +
                            ") AS A WHERE CLI.Id = "+idCliente+" AND LT.Id = "+idLote;


            return contexto.Database.SqlQuery<clsInformacionPagoLote>(query).FirstOrDefault();
        }

       


    }
}
