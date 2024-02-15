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
            string query =  "SELECT \r\n"+
                            "CL.Id AS ContratoId, CL.Folio, \r\n"+
                            "CLI.Clave as ClaveCliente, (PER.Nombres + ' ' + PER.Apellidos) AS ClienteNombre, \r\n"+
                            "CLI.Id as ClienteId, LT.Identificador AS ClaveLote, ZN.Nombre as ZonaLote, \r\n"+
                            "LT.Id as LoteId, ZN.Id as ZonaLoteId,  \r\n"+
                            "CL.PrecioInicial as PrecioLote, CL.NoPagos, CL.DiaPago,  \r\n"+
                            "CL.FechaArrendamiento as FechaEmision,  \r\n"+
                            "(PER.Nombres + ' ' + PER.Apellidos) as UsuarioRealizo, CL.FechaReimpresion, \r\n"+
                            "CL.SOCIOSId as SocioId \r\n" +
                            "FROM CLIENTELOTE AS CL \r\n"+
                            "JOIN CLIENTE CLI ON CL.CLIENTEId = CLI.Id \r\n"+
                            "JOIN PERSONA PER ON CLI.PERSONAId = PER.Id \r\n"+
                            "JOIN LOTE LT ON CL.LOTEId = LT.Id \r\n"+
                            "JOIN ZONA ZN ON LT.ZONAId = ZN.Id \r\n"+
                            "JOIN USUARIO USOP ON CL.USUARIOOperacionId = USOP.Id \r\n"+
                            "JOIN PERSONA PERUSOP ON USOP.PERSONAId = PERUSOP.Id \r\n"+
                            "WHERE CL.Folio = '"+folio+"'";

            return contexto.Database.SqlQuery<clsContratoCliente>(query).FirstOrDefault();
        }
        public List<clsContratoCliente> ListarContratosCliente(string folio)
        {
            string query = "SELECT \r\n" +
                            "CL.Id AS ContratoId, CL.Folio, \r\n" +
                            "CLI.Clave as ClaveCliente, (PER.Nombres + ' ' + PER.Apellidos) AS ClienteNombre, \r\n" +
                            "CLI.Id as ClienteId, LT.Identificador AS ClaveLote, ZN.Nombre as ZonaLote, \r\n" +
                            "LT.Id as LoteId, ZN.Id as ZonaLoteId,  \r\n" +
                            "CL.PrecioInicial as PrecioLote, CL.NoPagos, CL.DiaPago,  \r\n" +
                            "CL.FechaArrendamiento as FechaEmision,  \r\n" +
                            "(PER.Nombres + ' ' + PER.Apellidos) as UsuarioRealizo, CL.FechaReimpresion, \r\n" +
                            "CL.SOCIOSId as SocioId \r\n" +
                            "FROM CLIENTELOTE AS CL \r\n" +
                            "JOIN CLIENTE CLI ON CL.CLIENTEId = CLI.Id \r\n" +
                            "JOIN PERSONA PER ON CLI.PERSONAId = PER.Id \r\n" +
                            "JOIN LOTE LT ON CL.LOTEId = LT.Id \r\n" +
                            "JOIN ZONA ZN ON LT.ZONAId = ZN.Id \r\n" +
                            "JOIN USUARIO USOP ON CL.USUARIOOperacionId = USOP.Id \r\n" +
                            "JOIN PERSONA PERUSOP ON USOP.PERSONAId = PERUSOP.Id";

            return contexto.Database.SqlQuery<clsContratoCliente>(query).ToList();
        }      

    

      
    }
}
