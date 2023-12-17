using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAPADATOS.ADO.SISTEMA;
using CAPADATOS.Entidades;

namespace CAPALOGICA.LOGICAS.BUSQUEDA
{
    public class busModulosLogica
    {

        private ModulosADO contexto;
        public List<clsModulo> LstModulo;
        public List<clsModulo> LstModuloAux;

        public int index = -1;
        public int indexAux = -1;
        public int Column = 0;

        public busModulosLogica()
        {
            contexto = new ModulosADO();
        }

        public void ListarRegistros()
        {
            LstModulo = contexto.ListarModulos();   
        }

        public clsModulo ObtenerRegistro(int id)
        {
            return contexto.ObtenerModulo(id);  
        }

        public bool Filtrar(int column, string termino)
        {
            if (LstModuloAux == null) LstModuloAux = LstModulo;

            switch (column)
            {
                case 1:
                    index = LstModuloAux.FindIndex(x => x.Nombre.StartsWith(termino));
                    break;
                case 2:
                    index = LstModuloAux.FindIndex(x => x.Ruta.ToString().StartsWith(termino));
                    break;
                case 4:
                    index = LstModuloAux.FindIndex(x => x.ModuloPadre.StartsWith(termino));
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
                    LstModuloAux = LstModulo.OrderBy(x => x.Nombre).ThenBy(x => x.Ruta).ThenBy(x => x.ModuloPadre).ToList();
                    break;
                case 2:
                    LstModuloAux = LstModulo.OrderBy(x => x.Ruta).ThenBy(x => x.ModuloPadre).ThenBy(x => x.Nombre).ThenBy(x => x.Nombre).ToList();
                    break;
                case 3:
                    LstModuloAux = LstModulo.OrderBy(x => x.ModuloPadre).ThenBy(x => x.Nombre).ThenBy(x => x.Ruta).ThenBy(x => x.Nombre).ToList();
                    break;

                default:
                    LstModuloAux = LstModulo.OrderBy(x => x.Nombre).ThenBy(x => x.Ruta).ThenBy(x => x.ModuloPadre).ToList();
                    break;

            }
        }






    }
}
