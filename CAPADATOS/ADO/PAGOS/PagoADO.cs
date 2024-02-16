using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.ADO.PAGOS
{
    public class PagoADO : IDisposable
    {
        private DB_JAADEEntities contexto;

        public PagoADO()
        {
            contexto = new DB_JAADEEntities();
        }

        public void Insertar(PAGO entidad)
        {
            contexto.PAGO.Add(entidad);
        }

        public void Guardar()
        {
            contexto.SaveChanges();
        }

        public void Eliminar(PAGO entidad)
        {
            contexto.PAGO.Remove(entidad);
        }

        public List<PAGO> Listar()
        {
            return contexto.PAGO.ToList();
        }

        public PAGO Obtener(int id)
        {
            return contexto.PAGO.FirstOrDefault(x => x.Id == id);
        }
        public void Dispose()
        {
            contexto.Dispose();
        }

        public clsPagoData ObtenerPagoClienteFolio(string folio)
        {
            string query = "";

            return contexto.Database.SqlQuery<clsPagoData>(query).FirstOrDefault();
        }
        public List<clsPagoData> ListarPagoCliente(string folio)
        {
            string query = "";

            return contexto.Database.SqlQuery<clsPagoData>(query).ToList();
        }




    }
}

