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
                            "CL.SOCIOSId as SocioId, \r\n" +
                            "(SELECT COUNT(*) FROM PAGO WHERE PAGO.ContratoId = CL.Id) as PagosRealizados, \r\n" +
                            "CL.NoPagosGracia, CL.PagoInicial, EDO.Id AS EstadoId, EDO.Nombre as Estado, \r\n" +
                            "cl.Observacion, cl.MontoGracia \r\n" +
                            "FROM CLIENTELOTE AS CL \r\n" +
                            "JOIN CLIENTE CLI ON CL.CLIENTEId = CLI.Id \r\n"+
                            "JOIN PERSONA PER ON CLI.PERSONAId = PER.Id \r\n"+
                            "JOIN LOTE LT ON CL.LOTEId = LT.Id \r\n"+
                            "JOIN ZONA ZN ON LT.ZONAId = ZN.Id \r\n"+
                            "JOIN USUARIO USOP ON CL.USUARIOOperacionId = USOP.Id \r\n"+
                            "JOIN PERSONA PERUSOP ON USOP.PERSONAId = PERUSOP.Id \r\n" +
                            "JOIN ESTADO EDO ON CL.ESTADOId = EDO.Id \r\n"+
                            "WHERE CL.Folio = '"+folio+"'";

            return contexto.Database.SqlQuery<clsContratoCliente>(query).FirstOrDefault();
        }
        public List<clsContratoCliente> ListarContratosCliente()
        {
            string query = "SELECT \r\n" +
                            "CL.Id AS ContratoId, CL.Folio, \r\n" +
                            "CLI.Clave as ClaveCliente, (PER.Nombres + ' ' + PER.Apellidos) AS ClienteNombre, \r\n" +
                            "CLI.Id as ClienteId, LT.Identificador AS ClaveLote, ZN.Nombre as ZonaLote, \r\n" +
                            "LT.Id as LoteId, ZN.Id as ZonaLoteId,  \r\n" +
                            "CL.PrecioInicial as PrecioLote, CL.NoPagos, CL.DiaPago,  \r\n" +
                            "CL.FechaArrendamiento as FechaEmision,  \r\n" +
                            "(PER.Nombres + ' ' + PER.Apellidos) as UsuarioRealizo, CL.FechaReimpresion, \r\n" +
                            "CL.SOCIOSId as SocioId, \r\n" +
                            "(SELECT COUNT(*) FROM PAGO WHERE PAGO.ContratoId = CL.Id) as PagosRealizados, \r\n" +
                            "cl.NoPagosGracia, cl.PagoInicial, EDO.Id AS EstadoId, EDO.Nombre as Estado, \r\n" +
                            "cl.Observacion, cl.MontoGracia \r\n"+
                            "FROM CLIENTELOTE AS CL \r\n" +
                            "JOIN CLIENTE CLI ON CL.CLIENTEId = CLI.Id \r\n" +
                            "JOIN PERSONA PER ON CLI.PERSONAId = PER.Id \r\n" +
                            "JOIN LOTE LT ON CL.LOTEId = LT.Id \r\n" +
                            "JOIN ZONA ZN ON LT.ZONAId = ZN.Id \r\n" +
                            "JOIN USUARIO USOP ON CL.USUARIOOperacionId = USOP.Id \r\n" +
                            "JOIN PERSONA PERUSOP ON USOP.PERSONAId = PERUSOP.Id \r\n" +
                            "JOIN ESTADO EDO ON CL.ESTADOId = EDO.Id \r\n" +
                            "ORDER BY 1 DESC";

            return contexto.Database.SqlQuery<clsContratoCliente>(query).ToList();
        }

        public clsObjMontoGracia CalcularMontoGracia(string folioContrato)
        {
            string query = "SELECT A.*, \r\n"+
                            "((A.PrecioInicial - A.MontoAcumuladoDado) * 1.25) AS MontoGracia, \r\n"+
                            " (((A.PrecioInicial - A.MontoAcumuladoDado) * 1.25) / A.NoPagosGracia ) AS MontoMensualGracia \r\n"+
                            "FROM( \r\n"+
                            "SELECT \r\n"+
                            "CL.Id as ContratoId, \r\n"+
                            "CL.Folio as FolioContrato, \r\n"+
                            "(SELECT COUNT(*) FROM PAGO WHERE PAGO.ContratoId = CL.Id) AS NoPagosDados, \r\n"+
                            "(SELECT SUM(PAGO.Monto) FROM PAGO WHERE PAGO.ContratoId = CL.Id) AS MontoAcumuladoDado, \r\n"+
                            "(SELECT((CL.PrecioInicial - CL.PagoInicial) / CL.NoPagos * 12)) AS MontoAnualMinimo, \r\n"+
                            "Cl.NoPagosGracia, CL.PrecioInicial, \r\n"+
                            "( \r\n"+
                            " SELECT DATEDIFF(MONTH, CL.FechaArrendamiento, CONVERT(date, GETDATE())) \r\n"+
                            ") AS NoPagoActual \r\n"+
                            "FROM CLIENTELOTE CL \r\n" +
                            "WHERE CL.Folio = '"+folioContrato+"'"+
                            ") AS A"; 
            return contexto.Database.SqlQuery<clsObjMontoGracia>(query).FirstOrDefault();
        }
    }
}
