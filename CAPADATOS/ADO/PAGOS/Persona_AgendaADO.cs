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

        public void Eliminar(PERSONA_AGENDA entidad)
        {
            contexto.PERSONA_AGENDA.Remove(entidad);
        }

        public List<PERSONA_AGENDA> Listar()
        {
            return contexto.PERSONA_AGENDA.ToList();
        }
    }
}
