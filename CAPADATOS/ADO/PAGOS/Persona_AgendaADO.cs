using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.ADO.PAGOS
{
    public class Persona_AgendaADO:IDisposable
    {
        private DB_JAADEEntities contexto;

        public Persona_AgendaADO()
        {
            contexto = new DB_JAADEEntities();
        }

        public void Insertar(PERSONA_AGENDA entidad)
        {
            contexto.PERSONA_AGENDA.Add(entidad);
        }

        public void Guardar()
        {
            contexto.SaveChanges();
        }
        public PERSONA_AGENDA Obtener(int AgendaId)
        {
            return contexto.PERSONA_AGENDA.FirstOrDefault(x => x.AGENDAId == AgendaId);
        }

        public void Eliminar(PERSONA_AGENDA entidad)
        {
            contexto.PERSONA_AGENDA.Remove(entidad);
        }

        public List<PERSONA_AGENDA> Listar()
        {
            return contexto.PERSONA_AGENDA.ToList();
        }

        public List<clsAGENDACLIENTE> ListarAgendaContactoCliente(string claveCliente)
        {
            string query = "SELECT  \r\n"+
                        "ag.Id, AG.Tipo AS TIPOId, \r\n" +
                        "CASE WHEN(AG.Tipo = 1) THEN 'TELEFONO' \r\n"+
                            "ELSE(CASE WHEN(AG.Tipo = 2) THEN 'DIRECCION' ELSE 'CORREO ELECTRONICO' END) END AS Tipo, \r\n"+
                        "ag.Valor, pag.Id as RelacionId \r\n"+
                        "FROM CLIENTE CLI \r\n"+
                        "JOIN PERSONA_AGENDA PAG ON CLI.PERSONAId = PAG.PERSONAId \r\n"+
                        "JOIN AGENDA AG ON PAG.AGENDAId = AG.Id \r\n"+
                        "WHERE CLI.Clave = '"+claveCliente+"'";
            return contexto.Database.SqlQuery<clsAGENDACLIENTE>(query).ToList();
        }

        public List<clsAGENDACLIENTE>ListarAgendaCliente(int clienteId)
        {
            string query = "SELECT \r\n" +
                            "ag.Id, AG.Tipo AS TIPOId, \r\n" +
                            "CASE WHEN(AG.Tipo = 1) THEN 'TELEFONO' \r\n" +
                            "ELSE(CASE WHEN(AG.Tipo = 2) THEN 'DIRECCION' ELSE 'CORREO ELECTRONICO' END) END AS Tipo, \r\n" +
                            "ag.Valor, pa.Id as RelacionId \r\n" +
                            "FROM CLIENTE CLI \r\n" +
                            "JOIN PERSONA PER ON CLI.PERSONAId = PER.Id \r\n"+
                            "JOIN PERSONA_AGENDA PA ON PER.Id = PA.PERSONAId \r\n"+
                            "JOIN AGENDA AG ON PA.AGENDAId = AG.Id \r\n"+
                            "WHERE CLI.Id = "+clienteId;

            return contexto.Database.SqlQuery<clsAGENDACLIENTE>(query).ToList();
        }

        public void Dispose()
        {
            contexto.Dispose();
        }

        public AGENDA ObtenerDireccionCliente(string direccion, int clienteId)
        {
            string query = "select top 1\r\n" +
                            "ag.*\r\n" +
                            "from CLIENTE  cli \r\n" +
                            "join PERSONA_AGENDA pa on cli.PERSONAId = pa.PERSONAId\r\n" +
                            "join AGENDA ag on pa.AGENDAId = ag.Id\r\n" +
                            "where cli.Id = "+clienteId+" and  ag.Valor = '"+direccion+"'";

            return contexto.Database.SqlQuery<AGENDA>(query).FirstOrDefault();
        }
    }
}
