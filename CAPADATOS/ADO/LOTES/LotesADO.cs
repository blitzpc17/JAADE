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
                        "LT.CEste, Lt.COeste, LT.FechaRegistro, LT.Precio, LT.Manzana \r\n" +
                        "FROM CLIENTE_LOTE cl \r\n" +
                        "JOIN CLIENTE cli ON cl.CLIENTEId = cli.Id \r\n" +
                        "JOIN LOTE lt ON cl.LOTEId = lt.Id \r\n" +
                        "JOIN ZONA ZN ON lt.ZONAId = zn.Id \r\n"+
                        "WHERE cli.Id = "+id;
            }
            else
            {
                query = "SELECT " +
                           "LT.Id, LT.Identificador, ZN.Nombre AS Zona, zn.Id as ZonaId, \r\n" +
                           "LT.MNorte, LT.MSur, LT.MEste, LT.MOeste, LT.CNorte, LT.CSur, \r\n" +
                           "LT.CEste, Lt.COeste, LT.FechaRegistro, LT.Precio, LT.Manzana \r\n" +
                           "FROM ZONA AS ZN " +
                           "JOIN LOTE AS LT ON ZN.Id = LT.ZONAId " +
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
                           "LT.CEste, Lt.COeste, LT.FechaRegistro, LT.Precio, LT.Manzana " +
                           "FROM ZONA AS ZN " +
                           "JOIN LOTE AS LT ON ZN.Id = LT.ZONAId " +
                           "WHERE LT.Id = " + loteId;

            return contexto.Database.SqlQuery<clsLotes>(query).FirstOrDefault();
        }

       


    }
}
