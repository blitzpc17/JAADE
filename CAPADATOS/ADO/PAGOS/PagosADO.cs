using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.ADO.PAGOS
{
    public class PagosADO:IDisposable
    {
        private DB_JAADEEntities contexto;

        public PagosADO()
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

        public clsPago ObtenerDataPago(int id)
        {
            var query = "SELECT \r\n "+
                        "PG.NumeroReferencia, PG.FechaEmison,  \r\n "+
                        "CLI.Id AS ClienteId, (per.Nombres + ' ' + per.ApellidoPaterno + ' ' + per.ApellidoMaterno) as Cliente, \r\n "+
                        "LT.Id as LoteId, LT.Identificador as IdentificadorLote, LT.ZONAId as ZonaId, ZN.Nombre as Zona, LT.Manzana, \r\n "+
                        "PG.Monto, us.Id as UsuarioRecibeId, (PERUS.Nombres + ' ' + PERUS.ApellidoPaterno + ' ' + PERUS.ApellidoMaterno) as Usuario \r\n "+
                        "FROM PAGO PG \r\n "+
                        "JOIN CLIENTE CLI ON PG.CLIENTEId = CLI.Id \r\n "+
                        "JOIN PERSONA PER ON CLI.PERSONAId = PER.Id \r\n "+
                        "JOIN CLIENTE_LOTE CLT ON CLI.Id = CLT.CLIENTEId \r\n "+
                        "JOIN LOTE LT ON CLT.LOTEId = LT.Id \r\n "+
                        "JOIN ZONA ZN ON LT.ZONAId = ZN.Id \r\n"+
                        "JOIN USUARIO US ON PG.USUARIORECIBEPAGOId = US.Id \r\n "+
                        "JOIN PERSONA PERUS ON US.PERSONAId = PERUS.Id \r\n "+
                        "WHERE PG.Id = " + id;

            return contexto.Database.SqlQuery<clsPago>(query).FirstOrDefault();
        }

        public List<clsPago> ListarPagos(int? clienteId)
        {
            string query = "";
            if (clienteId!=null)
            {
                query = "SELECT \r\n " +
                    "PG.Id as PagoId, PG.NumeroReferencia, PG.FechaEmison,  \r\n " +
                    "CLI.Id AS ClienteId, (per.Nombres + ' ' + per.ApellidoPaterno + ' ' + per.ApellidoMaterno) as Cliente, \r\n " +
                    "LT.Id as LoteId, LT.Identificador as IdentificadorLote, LT.ZONAId as ZonaId, ZN.Nombre as Zona, LT.Manzana, \r\n " +
                    "PG.Monto, us.Id as UsuarioRecibeId, (PERUS.Nombres + ' ' + PERUS.ApellidoPaterno + ' ' + PERUS.ApellidoMaterno) as Usuario \r\n " +
                    "FROM PAGO PG \r\n " +
                    "JOIN CLIENTE CLI ON PG.CLIENTEId = CLI.Id \r\n " +
                    "JOIN PERSONA PER ON CLI.PERSONAId = PER.Id \r\n " +
                    "JOIN CLIENTE_LOTE CLT ON CLI.Id = CLT.CLIENTEId \r\n " +
                    "JOIN LOTE LT ON CLT.LOTEId = LT.Id \r\n " +
                    "JOIN ZONA ZN ON LT.ZONAId = ZN.Id \r\n " +
                    "JOIN USUARIO US ON PG.USUARIORECIBEPAGOId = US.Id \r\n " +
                    "JOIN PERSONA PERUS ON US.PERSONAId = PERUS.Id \r\n" +
                    "WHERE CLI.Id = "+clienteId;
            }
            else
            {
                query = "SELECT \r\n " +
                     "PG.Id as PagoId, PG.NumeroReferencia, PG.FechaEmison,  \r\n " +
                     "CLI.Id AS ClienteId, (per.Nombres + ' ' + per.ApellidoPaterno + ' ' + per.ApellidoMaterno) as Cliente, \r\n " +
                     "LT.Id as LoteId, LT.Identificador as IdentificadorLote, LT.ZONAId as ZonaId, ZN.Nombre as Zona, LT.Manzana, \r\n " +
                     "PG.Monto, us.Id as UsuarioRecibeId, (PERUS.Nombres + ' ' + PERUS.ApellidoPaterno + ' ' + PERUS.ApellidoMaterno) as Usuario \r\n " +
                     "FROM PAGO PG \r\n " +
                     "JOIN CLIENTE CLI ON PG.CLIENTEId = CLI.Id \r\n " +
                     "JOIN PERSONA PER ON CLI.PERSONAId = PER.Id \r\n " +
                     "JOIN CLIENTE_LOTE CLT ON CLI.Id = CLT.CLIENTEId \r\n " +
                     "JOIN LOTE LT ON CLT.LOTEId = LT.Id \r\n " +
                     "JOIN ZONA ZN ON LT.ZONAId = ZN.Id \r\n " +
                     "JOIN USUARIO US ON PG.USUARIORECIBEPAGOId = US.Id \r\n " +
                     "JOIN PERSONA PERUS ON US.PERSONAId = PERUS.Id";
            }
         

            return contexto.Database.SqlQuery<clsPago>(query).ToList();
        }





        public void Dispose()
        {
            contexto.Dispose();
        }
    }
}
