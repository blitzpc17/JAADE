using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.ADO.PAGOS
{
    public class ContactoClienteADO
    {

        private DB_JAADEEntities contexto;

        public ContactoClienteADO()
        {
            contexto = new DB_JAADEEntities();
        }

        public void Insertar(AGENDA entidad)
        {
            contexto.AGENDA.Add(entidad);
        }

        public void Guardar()
        {
            contexto.SaveChanges();
        }

        public void Eliminar(AGENDA entidad)
        {
            contexto.AGENDA.Remove(entidad);
        }

        public List<AGENDA> Listar()
        {
            return contexto.AGENDA.ToList();
        }

        public List<clsAgenda> ListarDatosContacto(int personaId)
        {
            var query = "SELECT \r\n" +
                       "AGE.Id, AGE.Tipo as TipoId, \r\n" +
                       "CASE WHEN AGE.Tipo = 1 THEN 'TELEFONO' ELSE(CASE WHEN AGE.Tipo = 2 THEN 'DIRECCION' ELSE 'CORREO ELECTRONICO' END) END AS Tipo, \r\n" +
                       "AGE.Valor as Dato, CLI.Id as ClienteId \r\n" +
                       "FROM PERSONA AS PER \r\n" +
                       "JOIN CLIENTE AS CLI ON PER.Id = CLI.PERSONAId \r\n" +
                       "JOIN PERSONA_AGENDA AS PERAGE ON PER.Id = PERAGE.PERSONAId \r\n" +
                       "JOIN AGENDA AS AGE ON PERAGE.Id = AGE.Id \r\n"+
                       "WHERE PER.Id = " + personaId;
            return contexto.Database.SqlQuery<clsAgenda>(query).ToList();
        }

        public clsAgenda ObtenerDatosContacto(int agendaId)
        {
            var query = "SELECT \r\n"+
                        "AGE.Id, AGE.Tipo as TipoId, \r\n"+
                        "CASE WHEN AGE.Tipo = 1 THEN 'TELEFONO' ELSE(CASE WHEN AGE.Tipo = 2 THEN 'DIRECCION' ELSE 'CORREO ELECTRONICO' END) END AS Tipo, \r\n"+
                        "AGE.Valor as Dato, CLI.Id as ClienteId \r\n"+
                        "FROM PERSONA AS PER \r\n"+
                        "JOIN CLIENTE AS CLI ON PER.Id = CLI.PERSONAId \r\n"+
                        "JOIN PERSONA_AGENDA AS PERAGE ON PER.Id = PERAGE.PERSONAId \r\n"+
                        "JOIN AGENDA AS AGE ON PERAGE.Id = AGE.Id \r\n" +
                        "WHERE AGE.Id = "+agendaId; 
            return contexto.Database.SqlQuery<clsAgenda>(query).FirstOrDefault();
        }

        public AGENDA Obtener(int id)
        {
            return contexto.AGENDA.FirstOrDefault(x => x.Id == id);
        }

        


    }
}
