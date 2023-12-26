using CAPADATOS;
using CAPADATOS.ADO.PAGOS;
using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPALOGICA.LOGICAS.PAGOS
{
    public class formAgendaClienteLogica
    {

        private ContactoClienteADO contextoContacto;

        public AGENDA ObjDatoContacto;
        public clsAgenda ObjDatoContactoData;
        public List<clsAgenda> LstDatosContacto;
        public List<clsAgenda> LstDatosContactoAux;
        public List<KeyValuePair<string, int>> LstTipos;
        public PERSONA_AGENDA ObjPersonaAgenda;
        public Persona_AgendaADO contextoPersonaAgenda;

        public int index = -1;
        public int indexAux = -1;
        public int Column = 0;
        public int? personaId = null;


        public formAgendaClienteLogica()
        {
            contextoContacto = new ContactoClienteADO();
            contextoPersonaAgenda = new Persona_AgendaADO();
        }

        public void InstanciarDatoContacto()
        {
            ObjDatoContacto = new AGENDA();
        }      

        public void ListarCatalogos()
        {
            LstTipos = new List<KeyValuePair<string, int>>();

            LstTipos.Add(new KeyValuePair<string, int>("TELEFONO", 1));
            LstTipos.Add(new KeyValuePair<string, int>("DIRECCION", 2));
            LstTipos.Add(new KeyValuePair<string, int>("CORREO ELECTRONICO", 3));
        }

        public void Guardar()
        {
            if (ObjDatoContacto.Id == 0)
            {
                contextoContacto.Insertar(ObjDatoContacto);
                contextoContacto.Guardar();
                ObjPersonaAgenda = new PERSONA_AGENDA
                {
                    PERSONAId = (int)personaId,
                    AGENDAId = ObjDatoContacto.Id
                };
                contextoPersonaAgenda.Insertar(ObjPersonaAgenda);
                contextoPersonaAgenda.Guardar();
                
            }
            contextoContacto.Guardar();
        }

        public AGENDA ObtenerAgenda(int id)
        {
            return contextoContacto.Obtener(id);
        }

        public bool Filtrar(int column, string termino)
        {
            if (LstDatosContactoAux == null) LstDatosContactoAux = LstDatosContacto;

            switch (column)
            {
                default:
                    index = LstDatosContactoAux.FindIndex(x => x.Tipo.StartsWith(termino));
                    break;

            }

            return (index >= 0);

        }

        public void Ordenar(int column)
        {
            switch (column)
            {
                default:
                    LstDatosContactoAux = LstDatosContacto.OrderBy(x => x.Tipo).ThenBy(x => x.Id).ToList();
                    break;

            }
        }


        public clsAgenda ObtenerDataContactoCliente(int id)
        {
            return contextoContacto.ObtenerDatosContacto(id);
        }

        public List<clsAgenda> ListarDataContactoCliente(int id)
        {
            return contextoContacto.ListarDatosContacto(id);
        }




    }
}
