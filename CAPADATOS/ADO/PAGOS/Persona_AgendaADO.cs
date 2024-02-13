using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.ADO.PAGOS
{
    public class Persona_AgendaADO
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
    }
}
