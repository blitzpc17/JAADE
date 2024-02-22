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
            string query = "SELECT  \r\n"+
                            "CL.Id as ContratoId, CL.Folio AS NoReferencia, CL.PrecioInicial AS PrecioLote,  \r\n"+
                            "CL.NoPagos, CL.DiaPago, Cl.PagoInicial,  \r\n"+
                            "CL.NoPagosGracia, CL.MontoGracia,  \r\n"+
                            "C.Id AS ClienteId, C.Clave AS ClaveCliente, (PC.Nombres + ' ' + pc.Apellidos) AS NombreCliente, \r\n"+
                            "LT.Id AS LoteId, LT.Identificador AS IdentificadorLote,  \r\n"+
                            "Zn.Id AS ZonaId, ZN.Nombre AS NombreZona, \r\n"+
                            "SOC.Id AS SocioId, SOC.Nombre as SocioNombre, \r\n"+
                            "EDO.Id AS EstadoId, EDO.Nombre AS NombreEstado, \r\n"+
                            "CRU.Id as ContratoReubicadoId, CRU.Folio As ContratoReubicado, \r\n" +
                            "EDO.Id AS EstadoId, EDO.Nombre AS EstadoNombre, \r\n"+
                            "CL.FechaArrendamiento AS FechaEmision, CL.FechaReimpresion, \r\n"+
                            "USOP.Id AS UsuarioOperacionId, (PERUSOP.Nombres + ' ' + PERUSOP.Apellidos) AS UsuarioOperacionNombre \r\n"+
                            "FROM CLIENTELOTE CL \r\n"+
                            "JOIN CLIENTE C ON CL.CLIENTEId = C.Id \r\n"+
                            "JOIN PERSONA PC ON C.PERSONAId = PC.Id \r\n"+
                            "JOIN LOTE LT ON CL.LOTEId = LT.Id \r\n"+
                            "JOIN ZONA ZN ON LT.ZONAId = ZN.Id \r\n"+
                            "JOIN ESTADO EDO ON CL.ESTADOId = EDO.Id \r\n"+
                            "JOIN USUARIO USOP ON CL.USUARIOOperacionId = USOP.Id \r\n"+
                            "JOIN PERSONA PERUSOP ON USOP.PERSONAId = PERUSOP.Id \r\n"+
                            "LEFT JOIN CLIENTES_SOCIOS CLISOC ON CL.CLIENTEId = CLISOC.CLIENTEId \r\n"+
                            "LEFT JOIN SOCIOS SOC ON CLISOC.SOCIOSId = SOC.Id \r\n"+
                            "LEFT JOIN CLIENTELOTE CRU ON cl.CLIENTELOTEId = CRU.Id \r\n" +
                            "WHERE CL.Folio = '"+folio+"'";   

            return contexto.Database.SqlQuery<clsContratoCliente>(query).FirstOrDefault();
        }

        public clsContratoCliente ObtenerContratoClienteXId(int contratoId)
        {
            string query = "SELECT  \r\n" +
                            "CL.Id as ContratoId, CL.Folio AS NoReferencia, CL.PrecioInicial AS PrecioLote,  \r\n" +
                            "CL.NoPagos, CL.DiaPago, Cl.PagoInicial,  \r\n" +
                            "CL.NoPagosGracia, CL.MontoGracia,  \r\n" +
                            "C.Id AS ClienteId, C.Clave AS ClaveCliente, (PC.Nombres + ' ' + pc.Apellidos) AS NombreCliente, \r\n" +
                            "LT.Id AS LoteId, LT.Identificador AS IdentificadorLote,  \r\n" +
                            "Zn.Id AS ZonaId, ZN.Nombre AS NombreZona, \r\n" +
                            "SOC.Id AS SocioId, SOC.Nombre as SocioNombre, \r\n" +
                            "EDO.Id AS EstadoId, EDO.Nombre AS NombreEstado, \r\n" +
                            "CRU.Id as ContratoReubicadoId, CRU.Folio As ContratoReubicado, \r\n" +
                            "EDO.Id AS EstadoId, EDO.Nombre AS EstadoNombre, \r\n" +
                            "CL.FechaArrendamiento AS FechaEmision, CL.FechaReimpresion, \r\n" +
                            "USOP.Id AS UsuarioOperacionId, (PERUSOP.Nombres + ' ' + PERUSOP.Apellidos) AS UsuarioOperacionNombre \r\n" +
                            "FROM CLIENTELOTE CL \r\n" +
                            "JOIN CLIENTE C ON CL.CLIENTEId = C.Id \r\n" +
                            "JOIN PERSONA PC ON C.PERSONAId = PC.Id \r\n" +
                            "JOIN LOTE LT ON CL.LOTEId = LT.Id \r\n" +
                            "JOIN ZONA ZN ON LT.ZONAId = ZN.Id \r\n" +
                            "JOIN ESTADO EDO ON CL.ESTADOId = EDO.Id \r\n" +
                            "JOIN USUARIO USOP ON CL.USUARIOOperacionId = USOP.Id \r\n" +
                            "JOIN PERSONA PERUSOP ON USOP.PERSONAId = PERUSOP.Id \r\n" +
                            "LEFT JOIN CLIENTES_SOCIOS CLISOC ON CL.CLIENTEId = CLISOC.CLIENTEId \r\n" +
                            "LEFT JOIN SOCIOS SOC ON CLISOC.SOCIOSId = SOC.Id \r\n" +
                            "LEFT JOIN CLIENTELOTE CRU ON cl.CLIENTELOTEId = CRU.Id \r\n" +
                            "WHERE CL.Id = " + contratoId ;

            return contexto.Database.SqlQuery<clsContratoCliente>(query).FirstOrDefault();
        }

        public List<clsContratoCliente> ListarContratosCliente()
        {
            string query = "SELECT \r\n"+
                            "CL.Id as ContratoId,  CL.Folio AS NoReferencia, CL.PrecioInicial AS PrecioLote, \r\n"+
                            "CL.NoPagos, CL.DiaPago, Cl.PagoInicial, \r\n"+
                            "CL.NoPagosGracia, CL.MontoGracia, \r\n"+
                            "C.Id AS ClienteId, C.Clave AS ClaveCliente, (PC.Nombres + ' ' + pc.Apellidos) AS NombreCliente,\r\n" +
                            "LT.Id AS LoteId, LT.Identificador AS IdentificadorLote, \r\n"+
                            "Zn.Id AS ZonaId, ZN.Nombre AS NombreZona,\r\n"+
                            "SOC.Id AS SocioId, SOC.Nombre as SocioNombre,\r\n" +
                            "EDO.Id AS EstadoId, EDO.Nombre AS NombreEstado,\r\n"+
                            "CRU.Id as ContratoReubicadoId, CRU.Folio As ContratoReubicado,\r\n"+
                            "EDO.Id AS EstadoId, EDO.Nombre AS EstadoNombre,\r\n"+
                            "CL.FechaArrendamiento AS FechaEmision, CL.FechaReimpresion,\r\n"+
                            "USOP.Id AS UsuarioOperacionId, (PERUSOP.Nombres + ' ' + PERUSOP.Apellidos) AS UsuarioOperacionNombre\r\n"+
                            "FROM CLIENTELOTE CL\r\n"+
                            "JOIN CLIENTE C ON CL.CLIENTEId = C.Id\r\n"+
                            "JOIN PERSONA PC ON C.PERSONAId = PC.Id\r\n"+
                            "JOIN LOTE LT ON CL.LOTEId = LT.Id\r\n"+
                            "JOIN ZONA ZN ON LT.ZONAId = ZN.Id\r\n"+
                            "JOIN ESTADO EDO ON CL.ESTADOId = EDO.Id\r\n"+
                            "JOIN USUARIO USOP ON CL.USUARIOOperacionId = USOP.Id\r\n"+
                            "JOIN PERSONA PERUSOP ON USOP.PERSONAId = PERUSOP.Id\r\n"+
                            "LEFT JOIN CLIENTES_SOCIOS CLISOC ON CL.CLIENTEId = CLISOC.CLIENTEId\r\n"+
                            "LEFT JOIN SOCIOS SOC ON CLISOC.SOCIOSId = SOC.Id\r\n"+
                            "LEFT JOIN CLIENTELOTE CRU ON cl.CLIENTELOTEId = CRU.Id"; 

            return contexto.Database.SqlQuery<clsContratoCliente>(query).ToList();
        }

        public clsArrendamientoLoteData ObtenerDatosContratoImpreso(string folioContrato)
        {
            string query = "SELECT "+
                            "CL.Folio AS NoReferencia, cl.FechaArrendamiento as FechaEmision, \r\n"+
                            "LT.Identificador as IdentificadorLote, Z.Nombre as ClaveZona, \r\n"+
                            "Z.Direccion as DomicilioZona, LT.Manzana, LT.CEste, LT.CNorte, LT.COeste, LT.CSur, \r\n"+
                            "LT.MEste, LT.MNorte, LT.MOeste, LT.MSur, LT.Precio AS PrecioLote, \r\n"+
                            "(PERCLI.Nombres + ' ' + PERCLI.Apellidos) AS NombreCliente, \r\n"+
                            "CL.DiaPago, CL.PagoInicial, CL.NoPagos, \r\n"+
                            "(PERCLI.Calle + ' ' + PERCLI.NoExt \r\n"+
                            "+ (CASE WHEN PERCLI.NoInt = null THEN ' ' ELSE ' ' + PERCLI.NoInt END) \r\n"+
                            "+' Col. ' + PERCLI.Colonia + ' C.P. ' + PERCLI.CodigoPostal + ' ' + PERCLI.Localidad + ' ' + PERCLI.Municipio + ' ' + PERCLI.EntidadFederativa) AS DomicilioCliente \r\n"+
                            "FROM CLIENTELOTE CL \r\n"+
                            "JOIN LOTE LT ON CL.LOTEId = LT.Id \r\n"+
                            "JOIN ZONA Z ON LT.ZONAId = Z.Id \r\n"+
                            "JOIN CLIENTE AS CLI ON CL.CLIENTEId = CLI.Id \r\n"+
                            "JOIN PERSONA AS PERCLI ON CLI.PERSONAId = PERCLI.Id \r\n"+
                            "WHERE CL.Folio = '"+folioContrato+"'"; 

           return contexto.Database.SqlQuery<clsArrendamientoLoteData>(query).FirstOrDefault();  
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
