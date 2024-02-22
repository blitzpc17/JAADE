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
                            "from CLIENTELOTE cl where cl.Folio = '"+folio+"'"; 

            return contexto.Database.SqlQuery<clsInformacionContratoPago>(query).FirstOrDefault();  

        }

        public PAGO ObtenerXFolio(string folio)
        {
            return contexto.PAGO.FirstOrDefault(x=>x.Folio == folio);
        }

        public List<clsBusquedaPago> ListarPagosContrato(string contrato = null)
        {
            string query = "SELECT " +
                            "PG.Id AS PagoId, PG.Folio, PG.FechaEmision,  \r\n" +
                            "CL.Id As ContratoId, CL.Folio as Contrato, (PERCLI.Nombres + ' ' + PERCLI.Apellidos) as Cliente, \r\n" +
                            "ZN.Nombre As Zona, LT.Identificador, PG.Monto \r\n" +
                            "FROM PAGO AS PG \r\n" +
                            "JOIN CLIENTELOTE AS CL ON PG.ContratoId = CL.Id \r\n" +
                            "JOIN LOTE LT ON CL.LOTEId = LT.Id \r\n" +
                            "JOIN ZONA ZN ON LT.ZONAId = ZN.Id \r\n" +
                            "JOIN CLIENTE CLI ON CL.CLIENTEId = CLI.Id \r\n" +
                            "JOIN PERSONA PERCLI ON CLI.PERSONAId = PERCLI.Id \r\n"
                            + ((string.IsNullOrEmpty(contrato)) ? "" : "WHERE CL.Folio = '"+contrato+"'");

            return contexto.Database.SqlQuery<clsBusquedaPago>(query).ToList();


        }




    }
}

