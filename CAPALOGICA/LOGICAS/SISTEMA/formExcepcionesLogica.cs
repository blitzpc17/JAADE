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
    public class formExcepcionesLogica
    {
        private ExcepcionADO contexto;

        public List<clsExcepciones> LstExcepciones;
        public List<clsExcepciones> LstExcepcionesAux;
        public clsExcepciones ObjExcepcion;
        public DateTime FechaRegistro;
        public int Column = -1, Index = -1, IndexAux = -1;

        public formExcepcionesLogica()
        {
            contexto = new ExcepcionADO();
        }

        public void Insertar(EXCEPCION entidad)
        {
            contexto.Insertar(entidad);
        }

        public void Guardar()
        {
            contexto.Guardar();
        }

        public void Eliminar(EXCEPCION entidad)
        {
            contexto.Eliminar(entidad);
        }

        public List<EXCEPCION> Listar()
        {
            return contexto.Listar();
        }

        public EXCEPCION Obtener(int id)
        {
            return contexto.Obtener(id);
        }

        public clsExcepciones ObtenerDataExcepcion(int id)
        {
            return contexto.ObtenerDataExcepcion(id);
        }

        public List<clsExcepciones> ListarExcepciones(DateTime fecha)
        {
           return contexto.ListarExcepciones(fecha);
        }

        public void Dispose()
        {
            contexto.Dispose();
        }

        public bool Filtrar(int column, string termino)
        {
            if (LstExcepcionesAux == null)
            {
                LstExcepciones = new List<clsExcepciones>();
                LstExcepcionesAux = LstExcepciones;
            }


            switch (column)
            {
                case 2:
                    Index = LstExcepcionesAux.FindIndex(x => x.Formulario.ToUpper().StartsWith(termino));
                    break;

                case 6:
                    Index = LstExcepcionesAux.FindIndex(x => x.NombreUsuario.StartsWith(termino));
                    break;

                default:
                    Index = LstExcepcionesAux.FindIndex(x => x.Formulario.StartsWith(termino));
                    break;

            }

            return (Index >= 0);

        }

        public void Ordenar(int column)
        {
            switch (column)
            {                
                case 2:
                    LstExcepcionesAux = LstExcepcionesAux.OrderBy(x => x.Formulario).ThenBy(x=>x.Fecha).ThenBy(x => x.NombreUsuario).ToList();
                    break;
                case 6:
                    LstExcepcionesAux = LstExcepcionesAux.OrderBy(x => x.NombreUsuario).ThenBy(x => x.Fecha).ThenBy(x => x.Formulario).ToList();
                    break;
                default:
                    LstExcepcionesAux = LstExcepcionesAux.OrderBy(x => x.Fecha).ThenBy(x => x.Formulario).ThenBy(x => x.NombreUsuario).ToList();
                    break;

            }


        }


    }
}
