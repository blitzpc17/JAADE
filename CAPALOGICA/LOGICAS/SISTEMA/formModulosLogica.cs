using CAPADATOS;
using CAPADATOS.ADO.SISTEMA;
using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPALOGICA.LOGICAS.SISTEMA
{
    public class formModulosLogica
    {
        private ModulosADO contextoModulo;
        public MODULO ObjModulo;
        public clsModulo ObjModuloData;
        public List<clsModulo> LstModulo;
        public List<clsModulo> LstModuloAux;

        public int index = -1;
        public int indexAux = -1;
        public int Column = 0;

        public formModulosLogica()
        {
            contextoModulo = new ModulosADO();
        }

        public void InstanciarRol()
        {
            ObjModulo = new MODULO();
        }

        public void Guardar()
        {
            if (ObjModulo.Id == 0)
            {
                contextoModulo.Insertar(ObjModulo);
            }

            contextoModulo.Guardar();

        }

        public void Listar()
        {
            LstModulo = contextoModulo.ListarModulos();
        }

        public MODULO Obtener(int id)
        {
            return contextoModulo.Obtener(id);
        }

        public clsModulo ObtenerData(int id)
        {
            return contextoModulo.ObtenerModulo(id);
        }        

        public void ListarModulos()
        {
            LstModulo = contextoModulo.ListarModulos();
            LstModuloAux = LstModulo;
        }

        public void Eliminar(MODULO entidad)
        {
            contextoModulo.Eliminar(entidad);
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
