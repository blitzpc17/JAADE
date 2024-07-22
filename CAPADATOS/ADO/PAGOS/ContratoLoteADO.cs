using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAPADATOS.ADO.PAGOS
{
    public class ContratoLoteADO : IDisposable
    {
        private DB_JAADEEntities contexto;

        public ContratoLoteADO()
        {
            contexto = new DB_JAADEEntities();
        }

        public void Insertar(CONTRATO entidad)
        {
            try
            {
                contexto.CONTRATO.Add(entidad);
            }
            catch (DbUpdateException ex)
            {
                throw;
            }
            
        }

        public void Guardar()
        {
            try
            {
                contexto.SaveChanges();
            }
            catch(DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            
        }

        public void Eliminar(CONTRATO entidad)
        {
            contexto.CONTRATO.Remove(entidad);
        }

        public List<CONTRATO> Listar()
        {
            return contexto.CONTRATO.ToList();
        }

        public CONTRATO Obtener(int id)
        {
            return contexto.CONTRATO.FirstOrDefault(x => x.Id == id);
        }        

        public void Dispose()
        {
            contexto.Dispose();
        }

        public clsContratoCliente ObtenerContratoClienteFolio(string folio)
        {
            string query = "SELECT " +
                            "CL.Id as ContratoId,  CL.Folio AS NoReferencia, CL.PrecioInicial AS PrecioLote,  \r\n" +
                            "CL.NoPagos, CL.DiaPago, Cl.PagoInicial,  \r\n" +
                            "CL.NoPagosGracia, CL.MontoGracia,  \r\n" +
                            "C.Id AS ClienteId, C.Clave AS ClaveCliente, (PC.Nombres + ' ' + pc.Apellidos) AS NombreCliente, \r\n" +
                            "    STUFF(( \r\n" +
                            "        SELECT ', ' + CAST(lot.Identificador AS VARCHAR(MAX)) \r\n" +
                            "        FROM LOTE AS lot \r\n" +
                            "        JOIN CONTRATO_LOTES ON lot.Id = CONTRATO_LOTES.LOTEId \r\n" +
                            "        WHERE CONTRATO_LOTES.CONTRATOId = cl.Id \r\n" +
                            "        FOR XML PATH('')), 1, 2, '') AS LotesRelacionados, \r\n" +
                            "(select top 1 lote.ZONAId from LOTE join CONTRATO_LOTES on lote.Id = CONTRATO_LOTES.LOTEId where CONTRATO_LOTES.CONTRATOId = CL.Id) as ZonaId, \r\n" +
                            "(select top 1  zona.Nombre from LOTE join ZONA on lote.ZONAId = ZONA.Id join CONTRATO_LOTES on lote.Id = CONTRATO_LOTES.LOTEId where CONTRATO_LOTES.CONTRATOId = CL.Id) as ZonaNombre, \r\n" +
                            "SOC.Id AS SocioId, SOC.Nombre as SocioNombre, \r\n" +
                            "EDO.Id AS EstadoId, EDO.Nombre AS NombreEstado, \r\n" +
                            "CL.FechaArrendamiento AS FechaEmision, CL.FechaReimpresion, \r\n" +
                            "USOP.Id AS UsuarioOperacionId, (PERUSOP.Nombres + ' ' + PERUSOP.Apellidos) AS UsuarioOperacionNombre, \r\n" +
                            "CL.ColindaNorte, CL.ColindaSur, CL.ColindaEste, CL.ColindaOeste, CL.MideNorte, CL.MideSur, CL.MideEste, CL.MideOeste, AG.Id as DomicilioClienteId, \r\n" +
                            "AG.Valor AS DomicilioCliente, CU.Id as ContratoOrigenId, CU.Folio as FolioContratoOrigen, CL.Observacion, \r\n" +
                            "(SELECT (SUM(PAGO.Monto)) FROM PAGO WHERE PAGO.ContratoId = CL.Id) as MontoDado, \r\n" +
                            "(SELECT(COUNT(PAGO.Monto)) FROM PAGO WHERE PAGO.ContratoId = CL.Id) as NoPagosDados, \r\n"+
                            "(SELECT TOP 1 PAGO.FechaEmision FROM PAGO WHERE PAGO.ContratoId = CL.Id ORDER BY 1 DESC) as FechaUltimoPago, \r\n" +
                            "(SELECT SUM(PAGO.Monto) FROM PAGO WHERE PAGO.ContratoId = CL.Id and PAGO.PagoOrdinario = 0 ) as MontoExtendidoDado, \r\n"+
                            "(SELECT COUNT(PAGO.Id) FROM PAGO WHERE PAGO.ContratoId = CL.Id and PAGO.PagoOrdinario = 0 ) as NoPagosExtendidosDados \r\n"+
                            "FROM CONTRATO CL \r\n" +
                            "JOIN CLIENTE C ON CL.CLIENTEId = C.Id \r\n" +
                            "JOIN PERSONA PC ON C.PERSONAId = PC.Id \r\n" +
                            "JOIN PERSONA_AGENDA PA ON PC.Id = PA.PERSONAId \r\n" +
                            "JOIN AGENDA AG ON PA.AGENDAId = AG.Id AND AG.Id = CL.AGENDAId \r\n" +
                            "JOIN ESTADO EDO ON CL.ESTADOId = EDO.Id \r\n" +
                            "JOIN USUARIO USOP ON CL.USUARIOOperacionId = USOP.Id \r\n" +
                            "JOIN PERSONA PERUSOP ON USOP.PERSONAId = PERUSOP.Id \r\n" +
                            "LEFT JOIN CLIENTES_SOCIOS CLISOC ON CL.CLIENTEId = CLISOC.CLIENTEId \r\n" +
                            "LEFT JOIN SOCIOS SOC ON CLISOC.SOCIOSId = SOC.Id \r\n" +
                            "LEFT JOIN CONTRATO CU ON CL.CONTRATOId = CU.Id \r\n"+
                            "WHERE CL.Folio = '" +folio+"'";   

            return contexto.Database.SqlQuery<clsContratoCliente>(query).FirstOrDefault();
        }

        public clsContratoCliente ObtenerContratoClienteXId(int contratoId)
        {
            string query = "SELECT " +
                           "CL.Id as ContratoId,  CL.Folio AS NoReferencia, CL.PrecioInicial AS PrecioLote,  \r\n" +
                           "CL.NoPagos, CL.DiaPago, Cl.PagoInicial,  \r\n" +
                           "CL.NoPagosGracia, CL.MontoGracia,  \r\n" +
                           "C.Id AS ClienteId, C.Clave AS ClaveCliente, (PC.Nombres + ' ' + pc.Apellidos) AS NombreCliente, \r\n" +
                           "    STUFF(( \r\n" +
                           "        SELECT ', ' + CAST(lot.Identificador AS VARCHAR(MAX)) \r\n" +
                           "        FROM LOTE AS lot \r\n" +
                           "        JOIN CONTRATO_LOTES ON lot.Id = CONTRATO_LOTES.LOTEId \r\n" +
                           "        WHERE CONTRATO_LOTES.CONTRATOId = cl.Id \r\n" +
                           "        FOR XML PATH('')), 1, 2, '') AS LotesRelacionados, \r\n" +
                           "(select top 1 lote.ZONAId from LOTE join CONTRATO_LOTES on lote.Id = CONTRATO_LOTES.LOTEId where CONTRATO_LOTES.CONTRATOId = CL.Id) as ZonaId, \r\n" +
                           "(select top 1  zona.Nombre from LOTE join ZONA on lote.ZONAId = ZONA.Id join CONTRATO_LOTES on lote.Id = CONTRATO_LOTES.LOTEId where CONTRATO_LOTES.CONTRATOId = CL.Id) as ZonaNombre, \r\n" +
                           "SOC.Id AS SocioId, SOC.Nombre as SocioNombre, \r\n" +
                           "EDO.Id AS EstadoId, EDO.Nombre AS NombreEstado, \r\n" +
                           "CL.FechaArrendamiento AS FechaEmision, CL.FechaReimpresion, \r\n" +
                           "USOP.Id AS UsuarioOperacionId, (PERUSOP.Nombres + ' ' + PERUSOP.Apellidos) AS UsuarioOperacionNombre, \r\n" +
                           "CL.ColindaNorte, CL.ColindaSur, CL.ColindaEste, CL.ColindaOeste, CL.MideNorte, CL.MideSur, CL.MideEste, CL.MideOeste, AG.Id as DomicilioClienteId, \r\n" +
                           "AG.Valor AS DomicilioCliente, CU.Id as ContratoOrigenId, CU.Folio as FolioContratoOrigen, CL.Observacion, \r\n" +
                           "(SELECT (SUM(PAGO.Monto)) FROM PAGO WHERE PAGO.ContratoId = CL.Id) as MontoDado, \r\n" +
                           "(SELECT(COUNT(PAGO.Monto)) FROM PAGO WHERE PAGO.ContratoId = CL.Id) as NoPagosDados, \r\n" +
                           "(SELECT TOP 1 PAGO.FechaEmision FROM PAGO WHERE PAGO.ContratoId = CL.Id ORDER BY 1 DESC) as FechaUltimoPago, \r\n" +
                            "(SELECT SUM(PAGO.Monto) FROM PAGO WHERE PAGO.ContratoId = CL.Id and PAGO.PagoOrdinario = 0 ) as MontoExtendidoDado, \r\n" +
                            "(SELECT COUNT(PAGO.Id) FROM PAGO WHERE PAGO.ContratoId = CL.Id and PAGO.PagoOrdinario = 0 ) as NoPagosExtendidosDados \r\n" +
                           "FROM CONTRATO CL \r\n" +
                           "JOIN CLIENTE C ON CL.CLIENTEId = C.Id \r\n" +
                           "JOIN PERSONA PC ON C.PERSONAId = PC.Id \r\n" +
                           "JOIN PERSONA_AGENDA PA ON PC.Id = PA.PERSONAId \r\n" +
                           "JOIN AGENDA AG ON PA.AGENDAId = AG.Id AND AG.Id = CL.AGENDAId \r\n" +
                           "JOIN ESTADO EDO ON CL.ESTADOId = EDO.Id \r\n" +
                           "JOIN USUARIO USOP ON CL.USUARIOOperacionId = USOP.Id \r\n" +
                           "JOIN PERSONA PERUSOP ON USOP.PERSONAId = PERUSOP.Id \r\n" +
                           "LEFT JOIN CLIENTES_SOCIOS CLISOC ON CL.CLIENTEId = CLISOC.CLIENTEId \r\n" +
                           "LEFT JOIN SOCIOS SOC ON CLISOC.SOCIOSId = SOC.Id \r\n" +
                           "LEFT JOIN CONTRATO CU ON CL.CONTRATOId = CU.Id \r\n"+
                           "WHERE CL.Id = " + contratoId ;

            return contexto.Database.SqlQuery<clsContratoCliente>(query).FirstOrDefault();
        }

        public List<clsContratoCliente> ListarContratosCliente()
        {
            string query = "SELECT "+
                            "CL.Id as ContratoId,  CL.Folio AS NoReferencia, CL.PrecioInicial AS PrecioLote,  \r\n"+
                            "CL.NoPagos, CL.DiaPago, Cl.PagoInicial,  \r\n"+
                            "CL.NoPagosGracia, CL.MontoGracia,  \r\n"+
                            "C.Id AS ClienteId, C.Clave AS ClaveCliente, (PC.Nombres + ' ' + pc.Apellidos) AS NombreCliente, \r\n"+
                            "    STUFF(( \r\n"+
                            "        SELECT ', ' + CAST(lot.Identificador AS VARCHAR(MAX)) \r\n"+
                            "        FROM LOTE AS lot \r\n"+
                            "        JOIN CONTRATO_LOTES ON lot.Id = CONTRATO_LOTES.LOTEId \r\n"+
                            "        WHERE CONTRATO_LOTES.CONTRATOId = cl.Id \r\n"+
                            "        FOR XML PATH('')), 1, 2, '') AS LotesRelacionados, \r\n"+
                            "(select top 1 lote.ZONAId from LOTE join CONTRATO_LOTES on lote.Id = CONTRATO_LOTES.LOTEId where CONTRATO_LOTES.CONTRATOId = CL.Id) as ZonaId, \r\n"+
                            "(select top 1  zona.Nombre from LOTE join ZONA on lote.ZONAId = ZONA.Id join CONTRATO_LOTES on lote.Id = CONTRATO_LOTES.LOTEId where CONTRATO_LOTES.CONTRATOId = CL.Id) as ZonaNombre, \r\n"+
                            "SOC.Id AS SocioId, SOC.Nombre as SocioNombre, \r\n"+
                            "EDO.Id AS EstadoId, EDO.Nombre AS NombreEstado, \r\n"+
                            "CL.FechaArrendamiento AS FechaEmision, CL.FechaReimpresion, \r\n"+
                            "USOP.Id AS UsuarioOperacionId, (PERUSOP.Nombres + ' ' + PERUSOP.Apellidos) AS UsuarioOperacionNombre, \r\n"+
                            "CL.ColindaNorte, CL.ColindaSur, CL.ColindaEste, CL.ColindaOeste, CL.MideNorte, CL.MideSur, CL.MideEste, CL.MideOeste, AG.Id as DomicilioClienteId, \r\n" +
                            "AG.Valor AS DomicilioCliente, CU.Id as ContratoOrigenId, CU.Folio as FolioContratoOrigen, CL.Observacion, \r\n"+
                            "(case when (SELECT (COUNT(PAGO.Monto)) FROM PAGO WHERE PAGO.ContratoId = CL.Id) = 0 THEN 0 ELSE (SELECT (SUM(PAGO.Monto)) FROM PAGO WHERE PAGO.ContratoId = CL.Id) END) as MontoDado,  \r\n" +
                            "(SELECT(COUNT(PAGO.Monto)) FROM PAGO WHERE PAGO.ContratoId = CL.Id) as NoPagosDados, \r\n" +
                            "(SELECT TOP 1 PAGO.FechaEmision FROM PAGO WHERE PAGO.ContratoId = CL.Id ORDER BY 1 DESC) as FechaUltimoPago, \r\n" +
                            "(SELECT SUM(PAGO.Monto) FROM PAGO WHERE PAGO.ContratoId = CL.Id and PAGO.PagoOrdinario = 0 ) as MontoExtendidoDado, \r\n" +
                            "(SELECT COUNT(PAGO.Id) FROM PAGO WHERE PAGO.ContratoId = CL.Id and PAGO.PagoOrdinario = 0 ) as NoPagosExtendidosDados \r\n" +
                            "FROM CONTRATO CL \r\n" +
                            "JOIN CLIENTE C ON CL.CLIENTEId = C.Id \r\n"+
                            "JOIN PERSONA PC ON C.PERSONAId = PC.Id \r\n"+
                            "JOIN PERSONA_AGENDA PA ON PC.Id = PA.PERSONAId \r\n"+
                            "JOIN AGENDA AG ON PA.AGENDAId = AG.Id AND AG.Id = CL.AGENDAId \r\n"+
                            "JOIN ESTADO EDO ON CL.ESTADOId = EDO.Id \r\n"+
                            "JOIN USUARIO USOP ON CL.USUARIOOperacionId = USOP.Id \r\n"+
                            "JOIN PERSONA PERUSOP ON USOP.PERSONAId = PERUSOP.Id \r\n"+
                            "LEFT JOIN CLIENTES_SOCIOS CLISOC ON CL.CLIENTEId = CLISOC.CLIENTEId \r\n"+
                            "LEFT JOIN SOCIOS SOC ON CLISOC.SOCIOSId = SOC.Id \r\n"+
                            "LEFT JOIN CONTRATO CU ON CL.CONTRATOId = CU.Id \r\n";

            return contexto.Database.SqlQuery<clsContratoCliente>(query).ToList();
        }

        public clsArrendamientoLoteData ObtenerDatosContratoImpreso(string folioContrato)
        {            

            string query = "SELECT \r\n"+
                            "C.Folio As NumeroReferencia, C.FechaArrendamiento As FechaEmision, \r\n"+
                            "STUFF(( \r\n"+
                            "        SELECT ', ' + CAST(LOTE.Identificador AS VARCHAR(MAX)) \r\n"+
                            "        FROM LOTE \r\n"+
                            "        JOIN CONTRATO_LOTES ON LOTE.Id = CONTRATO_LOTES.LOTEId \r\n"+
                            "        WHERE CONTRATO_LOTES.CONTRATOId = C.Id \r\n"+
                            "        FOR XML PATH('')), 1, 2, '') As LotesRelacionados, \r\n"+
                            "STUFF(( \r\n"+
                            "    SELECT ', ' + CAST(lot.NoLote AS VARCHAR(MAX)) \r\n"+
                            "    FROM LOTE AS lot \r\n"+
                            "    JOIN CONTRATO_LOTES ON lot.Id = CONTRATO_LOTES.LOTEId \r\n"+
                            "    WHERE CONTRATO_LOTES.CONTRATOId = C.Id \r\n"+
                            "    FOR XML PATH('')), 1, 2, '') AS NoLotesRelacionados, \r\n"+
                            "(SELECT TOP 1 Manzana FROM LOTE JOIN CONTRATO_LOTES ON LOTE.Id = CONTRATO_LOTES.LOTEId  WHERE CONTRATO_LOTES.CONTRATOId = C.Id  ) AS NoManzana, \r\n" +
                            "(PER.Nombres + ' ' + PER.Apellidos) As NombreCliente, \r\n" +
                            "    C.DiaPago, C.PagoInicial, C.NoPagos, C.NoPagosGracia As NoPagosExtendido, \r\n"+
                            "C.ColindaNorte, C.ColindaSur, C.ColindaEste, C.ColindaOeste, \r\n"+
                            "C.MideNorte, C.MideSur, C.MideEste, C.MideOeste, \r\n"+
                            "(SELECT  AGENDA.Valor FROM AGENDA WHERE AGENDA.Id = C.AGENDAId) as DomicilioCliente, \r\n"+
                            "(SELECT SUM(LOTE.Precio) FROM CONTRATO_LOTES JOIN LOTE ON CONTRATO_LOTES.LOTEId = LOTE.Id WHERE CONTRATO_LOTES.CONTRATOId = C.Id ) As TotalContrato, \r\n"+
                            "(select top 1  zona.Direccion from LOTE join ZONA on lote.ZONAId = ZONA.Id join CONTRATO_LOTES on lote.Id = CONTRATO_LOTES.LOTEId where CONTRATO_LOTES.CONTRATOId = C.Id ) as UbicacionZona \r\n" +
                            "FROM CONTRATO C \r\n"+
                            "JOIN CLIENTE CLI ON C.CLIENTEId = CLI.Id \r\n"+
                            "JOIN PERSONA PER ON CLI.PERSONAId = PER.Id \r\n"+
                            "WHERE C.Folio = '"+folioContrato+"'";

           return contexto.Database.SqlQuery<clsArrendamientoLoteData>(query).FirstOrDefault();  
        }

        public CONTRATO ObtenerContratoXFolio(string contrato)
        {
            return contexto.CONTRATO.FirstOrDefault(x=>x.Folio == contrato);
        }

        public CONTRATO ObtenerContratoXId(int contratoId)
        {
            return contexto.CONTRATO.FirstOrDefault(x => x.Id== contratoId);
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
                            "FROM CONTRATO CL \r\n" +
                            "WHERE CL.Folio = '"+folioContrato+"'"+
                            ") AS A"; 
            return contexto.Database.SqlQuery<clsObjMontoGracia>(query).FirstOrDefault();
        }

        public bool RelacionarLotesContrato(int contratoId, List<LOTE> lstLotes,int estadoContrato, int estadoLote)
        {
            string sql = "";

            if (estadoContrato == 9 || estadoContrato == 10)//vigente o atrasado
            {
                sql = "INSERT INTO CONTRATO_LOTES (CONTRATOId, LOTEId) VALUES ";
                int totalLotes = lstLotes.Count;
                foreach(var lt in lstLotes)
                {
                    sql += "("+contratoId+", "+lt.Id+" )";
                    totalLotes--;
                    if (totalLotes > 0)
                    {
                        sql += ",";
                    }
                }
                var result = contexto.Database.ExecuteSqlCommand(sql);
                if (result != lstLotes.Count) return false;

            }

            string lotes = string.Join(",", lstLotes.Select(x => x.Id).ToList());
            sql = "UPDATE LOTE SET ESTADOId = "+estadoLote+" WHERE Id in ("+lotes+");";

            var contenido = contexto.Database.ExecuteSqlCommand(sql);
            return contenido == lstLotes.Count();

        }


    }
}
