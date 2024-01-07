﻿using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.ADO.PAGOS
{
    public class AsignacionClienteLoteADO
    {
        private DB_JAADEEntities contexto;

        public AsignacionClienteLoteADO()
        {
            contexto = new DB_JAADEEntities();
        }

        public void Insertar(CLIENTE_LOTE entidad)
        {
            contexto.CLIENTE_LOTE.Add(entidad);
        }

        public void Guardar()
        {
            contexto.SaveChanges();
        }

        public void Eliminar(CLIENTE_LOTE entidad)
        {
            contexto.CLIENTE_LOTE.Remove(entidad);
        }

        public List<CLIENTE_LOTE> Listar()
        {
            return contexto.CLIENTE_LOTE.ToList();
        }

        public CLIENTE_LOTE Obtener(int id)
        {
            return contexto.CLIENTE_LOTE.FirstOrDefault(x => x.Id == id);
        }

        public clsClienteLote ObtenerAsignacion(int id)
        {
            var query = " " + id;

            return contexto.Database.SqlQuery<clsClienteLote>(query).FirstOrDefault();
        }

        public List<clsClienteLote> ListarAsignacionesClientes(int clienteId)
        {
            string query = "SELECT " +
                           "C.Id as ClienteId, zn.Id as ZonaId, LT.Id as LoteId, " +
                           "LT.Identificador as CodigoLote, ZN.Nombre AS ZonaNombre,  \r\n" +
                           "(per.Nombres+' '+per.ApellidoPaterno+' '+per.ApellidoMaterno) as Cliente, \r\n" +
                           "CL.FechaRegistro as FechaAsignacion, " +
                           "LT.Manzana, LT.Precio as PrecioLote, CL.PagoInicial, CL.NoPagos, CL.MontoRestante \r\n" +
                            "FROM \r\n" +
                            "CLIENTE_LOTE CL \r\n" +
                            "JOIN CLIENTE C ON CL.CLIENTEId = C.Id \r\n" +
                            "JOIN PERSONA AS per ON C.PERSONAId = per.Id \r\n" +
                            "JOIN LOTE LT ON CL.LOTEId = LT.Id \r\n" +
                            "JOIN ZONA ZN ON LT.ZONAId = ZN.Id \r\n" +
                            "WHERE C.Id = " + clienteId;

            return contexto.Database.SqlQuery<clsClienteLote>(query).ToList();

           
        }

        public clsClienteLote ObtenerAsignacionesLotes(int clienteId, int loteId)
        {
            string query = "SELECT " +
                           "C.Id as ClienteId, zn.Id as ZonaId, LT.Id as LoteId, " +
                           "LT.Identificador as CodigoLote, ZN.Nombre AS ZonaNombre,  \r\n" +
                           "(per.Nombres+' '+per.ApellidoPaterno+' '+per.ApellidoMaterno) as Cliente, \r\n" +
                           "CL.FechaRegistro as FechaAsignacion, " +
                           "LT.Manzana, LT.Precio as PrecioLote, CL.PagoInicial, CL.NoPagos, CL.MontoRestante \r\n" +
                            "FROM \r\n" +
                            "CLIENTE_LOTE CL \r\n" +
                            "JOIN CLIENTE C ON CL.CLIENTEId = C.Id \r\n" +
                            "JOIN PERSONA AS per ON C.PERSONAId = per.Id \r\n" +
                            "JOIN LOTE LT ON CL.LOTEId = LT.Id \r\n" +
                            "JOIN ZONA ZN ON LT.ZONAId = ZN.Id \r\n" +
                            "WHERE C.Id = " + clienteId + " AND LT.Id = "+loteId;

            return contexto.Database.SqlQuery<clsClienteLote>(query).FirstOrDefault();


        }


    }
}
