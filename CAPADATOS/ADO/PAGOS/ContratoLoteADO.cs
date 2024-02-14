using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.ADO.PAGOS
{
    public class ContratoLoteADO : IDisposable
    {
        private DB_JAADEEntities contexto;

        public ContratoLoteADO()
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
        public void Dispose()
        {
            contexto.Dispose();
        }

        public clsContratoCliente ObtenerContratoClienteFolio(string folio)
        {
            string query = "";
            return contexto.Database.SqlQuery<clsContratoCliente>(query).FirstOrDefault();
        }
        public List<clsContratoCliente> ListarContratosCliente(string folio)
        {
            string query = "";
            return contexto.Database.SqlQuery<clsContratoCliente>(query).ToList();
        }      

    

      
    }
}
