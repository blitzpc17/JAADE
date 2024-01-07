using CAPADATOS;
using CAPADATOS.ADO.LOTES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPALOGICA.LOGICAS.SISTEMA
{
    public class formZonaLogica
    {
        private ZonaADO contextoZona;
        public ZONA ObjZona;
        public List<ZONA> LstZona;
        public List<ZONA> LstZonaAux;

        public int index = -1;
        public int indexAux = -1;
        public int Column = 0;

        public formZonaLogica()
        {
            contextoZona = new ZonaADO();
        }

        public void InstanciarZona()
        {
            ObjZona = new ZONA();
        }

        public void Guardar()
        {
            if (ObjZona.Id == 0)
            {
                contextoZona.Insertar(ObjZona);
            }

            contextoZona.Guardar();

        }

        public void Listar()
        {
            LstZona = contextoZona.Listar();
            LstZonaAux = LstZona;
        }

        public ZONA Obtener(int id)
        {
            return contextoZona.Obtener(id);
        }

        public void Eliminar(ZONA entidad)
        {
            contextoZona.Eliminar(entidad);
        }


        public bool Filtrar(int column, string termino)
        {
            if (LstZonaAux == null) LstZonaAux = LstZona;

            switch (column)
            {
                case 1:
                    index = LstZonaAux.FindIndex(x => x.Nombre.StartsWith(termino));
                    break;

                default:
                    index = -1;
                    break;

            }

            return (index >= 0);

        }

        public void Ordenar(int column)
        {
            switch (column)
            {

                case 1:
                    LstZonaAux = LstZona.OrderBy(x => x.Nombre).ToList();
                    break;

                default:
                    LstZonaAux = LstZona.OrderBy(x => x.Nombre).ToList();
                    break;

            }
        }
    }
}
