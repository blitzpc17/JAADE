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

        public List<clsLotes> ListarLotes(int zonaId)
        {
            string query = "SELECT "+
                            "LT.Id, LT.Identificador, ZN.Nombre AS Zona, zn.Id ZonaId, "+
                            "LT.MNorte, LT.MSur, LT.MEste, LT.MOeste, LT.CNorte, LT.CSur, "+
                            "LT.CEste, Lt.COeste, LT.FechaRegistro, LT.Precio, LT.Manzana " +
                            "FROM ZONA AS ZN "+
                            "JOIN LOTE AS LT ON ZN.Id = LT.ZONAId " +
                            "WHERE ZN.Id = "+zonaId; 

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
