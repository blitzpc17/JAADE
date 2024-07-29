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

        public List<clsRepCorteCaja> ListarPagosPorFechaEmision(DateTime fechaInicio, DateTime fechaFin)
        {

            string query = "SELECT \r\n" +
                "P.Id AS PagoId, P.Folio as FolioPago, P.FechaEmision as FechaEmisionPago, C.Folio as FolioContrato,\r\n" +
                "STUFF(( \r\n" +
                    "SELECT ', ' + CAST(LOTE.Identificador AS VARCHAR(MAX)) \r\n" +
                    "FROM LOTE \r\n" +
                    "JOIN CONTRATO_LOTES ON LOTE.Id = CONTRATO_LOTES.LOTEId \r\n" +
                    "WHERE CONTRATO_LOTES.CONTRATOId = C.Id\r\n " +
                    "FOR XML PATH('')), 1, 2, '')  as IdentificadorLote, P.Monto, (PERREC.Nombres + ' ' + perrec.Apellidos) as NombreRecibio,\r\n" +
                "P.Observacion\r\n" +
                "FROM PAGO P\r\n" +
                "JOIN CONTRATO C ON P.ContratoId = C.Id\r\n" +             
                "JOIN USUARIO USREC ON P.USUARIORecibeId = USREC.Id\r\n" +
                "JOIN PERSONA PERREC ON USREC.PERSONAId = PERREC.Id\r\n" +
                "WHERE CAST(P.FechaEmision AS DATE ) >= '"+fechaInicio.ToString("yyyy-MM-dd")+"' AND CAST(P.FechaEmision AS DATE )  <= '"+fechaFin.ToString("yyyy-MM-dd")+"' ";

            return contexto.Database.SqlQuery<clsRepCorteCaja>(query).ToList();
        }

        public void Eliminar(PAGO entidad)
        {
            contexto.PAGO.Remove(entidad);
        }

        public List<PAGO> Listar()
        {
            return contexto.PAGO.ToList();
        }

        public List<clsPagoReciboEncabezado> ListarEncabezadosPagoXZona(int? zonaId)
        {
            string condicion = zonaId !=null? "WHERE ZN.Id = "+zonaId : "";
            string query= "SELECT \r\n" +
                "C.Id AS ContratoId, C.Folio as FolioContrato, \r\n" +
                "CLI.Id as ClienteId, CLI.Clave as ClaveCliente, \r\n" +
                "(PERCLI.Nombres + ' ' + PERCLI.Apellidos) as NombreCliente, \r\n" +
                "SOC.Nombre as Socio, \r\n" +
                "(SELECT TOP 1 ZONA.Id\r\nFROM CONTRATO_LOTES \r\n" +
                "JOIN LOTE ON CONTRATO_LOTES.LOTEId = LOTE.Id\r\nJOIN ZONA ON LOTE.ZONAId = ZONA.Id\r\n" +
                "WHERE CONTRATO_LOTES.CONTRATOId = C.Id\r\n" +
                ") as ZonaId, \r\n" +
                "(SELECT TOP 1 ZONA.Nombre \r\n" +
                "FROM CONTRATO_LOTES \r\n" +
                "JOIN LOTE ON CONTRATO_LOTES.LOTEId = LOTE.Id\r\n" +
                "JOIN ZONA ON LOTE.ZONAId = ZONA.Id\r\n" +
                "WHERE CONTRATO_LOTES.CONTRATOId = C.Id\r\n" +
                ") as ZonaNombre, \r\nSTUFF(( \r\n" +
                "SELECT ', ' + CAST(LOTE.Identificador AS VARCHAR(MAX)) \r\n " +
                "FROM LOTE \r\n JOIN CONTRATO_LOTES ON LOTE.Id = CONTRATO_LOTES.LOTEId \r\n" +
                "WHERE CONTRATO_LOTES.CONTRATOId = C.Id\r\n" +
                "FOR XML PATH('')), 1, 2, '') \r\n" +
                "as LoteIdentificador,  \r\n                            " +
                "C.PrecioInicial as PrecioLote, \r\n" +
                "C.NoPagos, \r\n" +
                "C.FechaArrendamiento,\r\n" +
                "C.Observacion\r\n" +
                "FROM \r\n" +
                "CONTRATO C \r\n" +
                "JOIN CLIENTE CLI ON  C.CLIENTEId = CLI.Id\r\n" +
                "JOIN PERSONA PERCLI ON CLI.PERSONAId = PERCLI.Id \r\n" +
                "LEFT JOIN SOCIOS SOC ON C.SOCIOSId = SOC.Id  " + condicion;



            return 
                contexto.Database.SqlQuery<clsPagoReciboEncabezado>(query).ToList();

        }

        public List<clsPagoReciboPartida>ListarPartidasPagoXZona(int? zonaId)
        {
            string condicion = zonaId != null ? "WHERE LT.ZonaId = "+zonaId : "";
            string query = "SELECT\r\n " +
                "CL.CONTRATOId AS ContratoId, PG.Id as PagoId, \r\n" +
                "PG.Folio, PG.NoPago, PG.Monto, PG.FechaEmision, \r\n" +
                "PG.Observacion \r\n" +
                "FROM PAGO PG \r\n" +
                "JOIN CONTRATO_LOTES CL ON PG.ContratoId = CL.CONTRATOId\r\n" +
                "JOIN LOTE LT ON CL.LOTEId = LT.Id\r\n" +
                "group by  CL.CONTRATOId, PG.Id, \r\n" +
                "PG.Folio, PG.NoPago, PG.Monto, PG.FechaEmision, \r\n" +
                "PG.Observacion " + condicion;

            return
                contexto.Database.SqlQuery<clsPagoReciboPartida>(query).ToList();

        }

        public List<clsTicketPartida> ObtenerPartidasPagoContrato(string noReferencia)
        {
            string query = "SELECT \r\n" +
                            "PG.NoPago as No, PG.Monto, PG.FechaEmision as Fecha, PG.Observacion \r\n" +
                            "FROM PAGO PG \r\n"+
                            "JOIN CONTRATO CL ON PG.ContratoId = CL.Id \r\n"+
                            "WHERE CL.Folio = '"+noReferencia+"'"; 

            return contexto.Database.SqlQuery<clsTicketPartida>(query).ToList();
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

        public clsInformacionContratoPago ObtenerInformacionPago(string folio)
        {
            string query= "select \r\n"+
                            "(select top 1 pago.NoPago from pago where pago.ContratoId = cl.Id and pago.PagoOrdinario = 1 order by pago.Id desc) as NoUltimoPago, \r\n"+
                            "(select sum(pago.Monto) from pago where pago.ContratoId = cl.id and pago.PagoOrdinario = 1) as SaldoFavor, \r\n"+
                            "((cl.PrecioInicial - cl.PagoInicial) / cl.NoPagos) as MontoMensualidad, \r\n"+
                            "(select count(*) from pago where pago.ContratoId = cl.Id and pago.PagoOrdinario = 1) as NoPagosRealizados, \r\n"+
                            "(select DATEDIFF(MONTH, cl.fechaArrendamiento, GETDATE())) as NoPagoActual, \r\n"+
                            "cl.NoPagos as NoPagosContrato, \r\n"+
                            "cl.NoPagosGracia, \r\n"+
                            "(select count(*) from pago where pago.ContratoId = cl.Id and pago.PagoOrdinario = 0) as NoPagosGraciaRealizados, \r\n"+
                            "(select DATEDIFF(MONTH, cl.fechaInicioProrroga, GETDATE())) as NoPagoProrrogaActual, \r\n"+
                            "(select top 1 pago.NoPago from pago where pago.ContratoId = cl.Id and pago.PagoOrdinario = 0 order by pago.Id desc) as NoUltimoPagoGraciaPago, \r\n"+
                            "(select sum(pago.Monto) from pago where pago.ContratoId = cl.id and pago.PagoOrdinario = 0) as SaldoProrrogaFavor, \r\n"+
                            "(cl.MontoGracia / cl.NoPagosGracia) as MensualidadGracia \r\n"+
                            "from CONTRATO cl where cl.Folio = '"+folio+"'"; 

            return contexto.Database.SqlQuery<clsInformacionContratoPago>(query).FirstOrDefault();  

        }

        public PAGO ObtenerXFolio(string folio)
        {
            return contexto.PAGO.FirstOrDefault(x=>x.Folio == folio);
        }

        public List<clsBusquedaPago> ListarPagosContrato(string contrato = null)
        {
            string query = "SELECT PG.Id AS PagoId, PG.Folio, PG.FechaEmision,  \r\n" +
                            "CL.Id As ContratoId, CL.Folio as Contrato, (PERCLI.Nombres + ' ' + PERCLI.Apellidos) as Cliente, \r\n" +
                            "( \r\n" +
                            "select top 1 zona.Nombre \r\nfrom contrato_lotes \r\n" +
                            "join lote on contrato_lotes.LoteId = lote.id \r\n" +
                            "join zona on lote.zonaid = zona.id \r\n" +
                            "where contrato_lotes.contratoid = cl.id ) as Zona,\r\n" +
                            "STUFF(( \r\n        " +
                                "SELECT ', ' + CAST(LOTE.Identificador AS VARCHAR(MAX)) \r\n        " +
                                "FROM LOTE \r\n        " +
                                "JOIN CONTRATO_LOTES ON LOTE.Id = CONTRATO_LOTES.LOTEId \r\n        " +
                                "WHERE CONTRATO_LOTES.CONTRATOId = CL.Id \r\n        " +
                            "FOR XML PATH('')), 1, 2, '') As LotesRelacionados, \r\n" +
                            "PG.Monto \r\n" +
                            "FROM PAGO AS PG \r\n" +
                            "JOIN CONTRATO AS CL ON PG.ContratoId = CL.Id \r\n" +
                            "JOIN CLIENTE CLI ON CL.CLIENTEId = CLI.Id \r\n" +
                            "JOIN PERSONA PERCLI ON CLI.PERSONAId = PERCLI.Id \r\n" +                          
                            ((string.IsNullOrEmpty(contrato)) ? "" : "WHERE CL.Folio = '"+contrato+"'") +
                            " \r\nORDER BY 1 DESC";

            return contexto.Database.SqlQuery<clsBusquedaPago>(query).ToList();


        }

        public clsCalculoMontoPagado CalcularMontoPagosDados(int idContrato)
        {
            string sql = "";

            return contexto.Database.SqlQuery<clsCalculoMontoPagado>(sql).FirstOrDefault();



        }
    }
}

